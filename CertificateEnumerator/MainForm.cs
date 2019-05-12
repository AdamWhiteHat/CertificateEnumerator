using System;
using System.IO;
using System.Net;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using CertificateEnumerator;

namespace CertificateEnumeratorGUI
{
    public partial class MainForm : Form
    {
        private CertificateRowCollection certificateRowCollection;
        private string publicKeysStoreOutputFilename = "PublicKeys.Store.Output.txt";
        private string publicKeysFolderOutputFilename = "PublicKeys.Folder.Output.txt";

        public MainForm()
        {
            InitializeComponent();
        }

        #region Form Event Handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            PopulateCells();
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
                PopulateCells();
            }
        }

        private void btnGetPublicKey_Click(object sender, EventArgs e)
        {
            List<string> publicKeys = certificateRowCollection.GetAllCertificatesPublicKeys();

            string filename = Utilities.EnsureFilenameNotExists(publicKeysStoreOutputFilename);
            File.WriteAllLines(filename, publicKeys);
        }

        private void btnCertRevocationLists_Click(object sender, EventArgs e)
        {
            List<string> installedCRLs = new List<string>();

            List<string> downloadedCRLs = certificateRowCollection.DownloadAllCertificatesRevocationListURLs();

            if (downloadedCRLs != null && downloadedCRLs.Count > 0)
            {
                installedCRLs = certificateRowCollection.InstallCertificatesRevocationLists(downloadedCRLs);
            }

			File.WriteAllLines(Path.Combine(Utilities.DownloadPath, "InstalledCerts.txt"), installedCRLs);
		}
				
		private void btnSearchFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    List<X509Certificate2> certs = Utilities.CertsFromFolder(dlg.SelectedPath);
                    CertificateRowCollection certRowCollection = CertificateRowCollection.FromList(certs);
                    List<string> publicKeys = certRowCollection.GetAllCertificatesPublicKeys();
    
                    string filename = Utilities.EnsureFilenameNotExists(publicKeysFolderOutputFilename);
                    File.WriteAllLines(filename, publicKeys);
                }
            }
        }

        #endregion

        private void Search(string value)
        {
            List<CertificateRow> newCertList = certificateRowCollection.Where(cr => cr.Contains(value)).ToList();
            SetDataSource(newCertList);
        }

        private void PopulateCells()
        {
            certificateRowCollection = new CertificateRowCollection();
            SetDataSource(certificateRowCollection);
        }

        private void SetDataSource(List<CertificateRow> certificateRow)
        {
            dataGridViewCertificates.DataSource = null;
            dataGridViewCertificates.DataSource = certificateRow;
            dataGridViewCertificates.Columns["HasErrors"].Visible = false;
            dataGridViewCertificates.Columns["ErrorText"].Visible = false;
            dataGridViewCertificates.Columns["ErrorProperty"].Visible = false;
            HighlightExpiredCertificateCells();
        }

        private void HighlightExpiredCertificateCells()
        {
            certificateRowCollection.VerifyAllCerts();

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
            }
        }

        #region Save As Methods

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

	}
}
