using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;

namespace CertificateManagement
{
    public partial class CertificateStoreView : Form
    {
        public CertificateRowCollection ViewDataSouce
        {
            get { return _viewDataSouce; }
            set
            {
                if (value == null)
                {
                    _viewDataSouce = new CertificateRowCollection(new List<CertificateRow>());
                }
                else
                {
                    _viewDataSouce = value;
                }

                if (_isLoaded)
                {
                    if (!_viewDataSouce.Any())
                    {
                        SetDataSource(null);
                    }
                    else
                    {
                        SetDataSource(_viewDataSouce);
                    }
                }
            }
        }
        private CertificateRowCollection _viewDataSouce = new CertificateRowCollection();

        private bool _isLoaded = false;

        public CertificateStoreView()
        {
            InitializeComponent();
        }

        public CertificateStoreView(CertificateRowCollection certificates)
            : this()
        {

            ViewDataSouce = certificates;
        }

        #region Form Event Handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetDataSource(ViewDataSouce);
            _isLoaded = true;
        }

        public void ResetVewToDefault()
        {
            tbSearch.Text = "";
            CertificateRowCollection certRowCollection = new CertificateRowCollection();
            ViewDataSouce = certRowCollection;
        }

        private void btnSaveCellsAs_Click(object sender, EventArgs e)
        {
            SaveCellsAs();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search(tbSearch.Text);
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Search(tbSearch.Text);
            }
            else if (e.KeyData == Keys.Escape)
            {
                ResetVewToDefault();
            }
        }

        private void dataGridViewCertificates_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                if (e.KeyData == Keys.S)
                {
                    SaveCellsAs();
                }
            }
            else if (e.KeyData == Keys.F5)
            {
                ResetVewToDefault();
            }
            else if (e.KeyData == Keys.Escape)
            {
                ResetVewToDefault();
            }
        }


        private void btnGetPublicKey_Click(object sender, EventArgs e)
        {
            string choosenFilename = DialogHelper.SaveFileDialog(Utilities.PublicKeysStoreOutputFilename, "Text Files|*.txt|All Files|*.*");
            if (string.IsNullOrWhiteSpace(choosenFilename))
            {
                return;
            }

            List<string> publicKeys = ViewDataSouce.GetAllCertificatesPublicKeyValues();

            string saveFilename = Utilities.EnsureFilenameNotExists(choosenFilename);
            File.WriteAllLines(saveFilename, publicKeys);
        }

        private void btnSearchFolder_Click(object sender, EventArgs e)
        {
            string selectedPath = DialogHelper.BrowseForFolderDialog();
            if (string.IsNullOrWhiteSpace(selectedPath))
            {
                return;
            }

            List<X509Certificate2> certs = Utilities.SearchForCertsInFolder(selectedPath);
            CertificateRowCollection certRowCollection = new CertificateRowCollection(certs);
            List<string> publicKeys = certRowCollection.GetAllCertificatesPublicKeyValues();

            string filename = Utilities.EnsureFilenameNotExists(Utilities.PublicKeysFolderOutputFilename);
            File.WriteAllLines(filename, publicKeys);

        }

        private void dataGridViewCertificates_Sorted(object sender, EventArgs e)
        {
            HighlightExpiredCertificateCells();
        }

        #endregion

        #region Feature Methods

        private void Search(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                ResetVewToDefault();
            }
            else
            {
                CertificateRowCollection newCertList = new CertificateRowCollection();
                List<CertificateRow> filteredCertList = newCertList.Where(cr => cr.ContainsString(value)).ToList();
                ViewDataSouce = new CertificateRowCollection(filteredCertList);
            }
        }

        private void SetDataSource(List<CertificateRow> certificateRows)
        {
            if (certificateRows == null || !certificateRows.Any())
            {
                dataGridViewCertificates.DataSource = null;
                return;
            }

            SortableBindingList<CertificateRow> boundList = new SortableBindingList<CertificateRow>(certificateRows);
            boundList.AllowEdit = false;
            boundList.AllowNew = false;
            boundList.AllowRemove = false;

            dataGridViewCertificates.DataSource = null;
            dataGridViewCertificates.DataSource = boundList;
            dataGridViewCertificates.Columns["HasErrors"].Visible = false;
            dataGridViewCertificates.Columns["ErrorText"].Visible = false;
            dataGridViewCertificates.Columns["ErrorProperty"].Visible = false;

            dataGridViewCertificates.Columns["IsChainVerified"].SortMode = DataGridViewColumnSortMode.Automatic;
            dataGridViewCertificates.Columns["HasPrivateKey"].SortMode = DataGridViewColumnSortMode.Automatic;

            HighlightExpiredCertificateCells();
        }

        private void HighlightExpiredCertificateCells()
        {
            ViewDataSouce.VerifyAllCerts();
            UpdateRowErrorHighlighting();
        }


        private void UpdateRowErrorHighlighting()
        {
            foreach (DataGridViewRow row in dataGridViewCertificates.Rows)
            {
                CertificateRow certRow = (CertificateRow)row.DataBoundItem;
                if (certRow.HasErrors)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                    DataGridViewCell cell = row.Cells[certRow.ErrorProperty];
                    if (cell != null)
                    {
                        cell.ErrorText = certRow.ErrorText;
                    }
                }
                else
                {

                }
            }
        }

        #endregion

        #region Save Methods

        private void SaveCellsAs()
        {
            if (dataGridViewCertificates.DataSource == null || dataGridViewCertificates.RowCount < 1)
            {
                MessageBox.Show("Nothing to save! Try hitting F5 to refresh.");
                return;
            }
            if (saveFileDialogSelectedCells.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = saveFileDialogSelectedCells.FileName;
                string extension = Path.GetExtension(filename);

                CopyCellsToClipboard();

                switch (extension)
                {
                    case ".txt":
                        SaveCells_AsTEXT(filename);
                        break;
                    case ".htm":
                    case ".html":
                        SaveCells_AsHTML(filename);
                        break;
                    case ".csv":
                        SaveCells_AsCSV(filename);
                        break;
                    case ".xls":
                    case ".xlsx":
                        SaveCells_AsXLSX(filename);
                        break;
                    default:
                        SaveCells_AsTEXT(filename);
                        break;
                }

                dataGridViewCertificates.ClearSelection();
            }
        }

        private void SaveCells_AsTEXT(string filename)
        {
            string clipBoard = PasteClipboard(TextDataFormat.Text);
            File.WriteAllText(filename, clipBoard);
        }

        private void SaveCells_AsCSV(string filename)
        {
            string clipBoard = PasteClipboard(TextDataFormat.CommaSeparatedValue);
            File.WriteAllText(filename, clipBoard);
        }

        private void SaveCells_AsHTML(string filename)
        {
            string clipBoard = PasteClipboard(TextDataFormat.Html);

            int start = clipBoard.IndexOf("<HTML>");
            clipBoard = clipBoard.Substring(start);

            File.WriteAllText(filename, clipBoard);
        }

        private void SaveCells_AsXLSX(string filename)
        {
            string fileTemp = Path.GetTempFileName();
            SaveCells_AsCSV(fileTemp);
            if (File.Exists(fileTemp))
            {
                //if (ExcelConverter.Convert(fileTemp, filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel9795))
                //{
                return;
                //}
            }
            MessageBox.Show("Unable to convert file to EXCEL file. Perhaps excel is not installed?");
        }

        IDataObject objectSave = null;
        private void CopyCellsToClipboard()
        {
            //DataGridViewSelectedCellCollection save_SelectedCells = dataGridViewCertificates.SelectedCells;

            // Choose whether to write header. You will want to do this for a CSV file.
            dataGridViewCertificates.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            // Select the cells we want to serialize.
            dataGridViewCertificates.SelectAll();
            // Copy (set clipboard)
            Clipboard.SetDataObject(dataGridViewCertificates.GetClipboardContent());
        }

        private string PasteClipboard(TextDataFormat format)
        {
            string result = Clipboard.GetText(format);
            // Restore the current state of the clipboard so the effect is seamless
            if (objectSave != null)
            {
                Clipboard.SetDataObject(objectSave);
            }
            return result;
        }

        #endregion

        #region Context Menu Events

        private CertificateRow GetSelectedRow()
        {
            var selectedRows = dataGridViewCertificates.SelectedRows.Cast<DataGridViewRow>().ToList();
            var selectedCertRows = selectedRows.Select(row => (CertificateRow)row.DataBoundItem).ToList();
            var selectedRow = selectedCertRows.First();
            return selectedRow;
        }

        private void dataGridViewCertificates_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dataGridViewCertificates.ClearSelection();
                dataGridViewCertificates.Rows[e.RowIndex].Selected = true;
            }
        }

        private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            CertificateRow row = GetSelectedRow();
            X509Certificate2UI.DisplayCertificate(row.certificate);
        }

        private void toolStripMenuItemExport_Click(object sender, EventArgs e)
        {
            string saveFile = DialogHelper.SaveFileDialog();
            if (!string.IsNullOrEmpty(saveFile))
            {
                CertificateRow row = GetSelectedRow();
                byte[] bytes = row.certificate.Export(X509ContentType.Cert);
                File.WriteAllBytes(saveFile, bytes);
            }
        }

        private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            DataObject dataObject = dataGridViewCertificates.GetClipboardContent();
            Clipboard.SetDataObject(dataObject, false);
        }

        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            CertificateRow row = GetSelectedRow();
            row.Remove();
        }

        private void toolStripMenuItemSelectAll_Click(object sender, EventArgs e)
        {
            dataGridViewCertificates.SelectAll();
        }

        #endregion

    }
}
