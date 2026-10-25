using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CertificateManagement
{
    public partial class CertificateRevocationListView : Form
    {
        public CertificateRowCollection sourceCertRowCollection;


        private bool _isLoaded = false;

        public CertificateRevocationRowCollection ViewDataSouce
        {
            get { return _viewDataSouce; }
            set
            {
                if (value == null)
                {
                    _viewDataSouce = new CertificateRevocationRowCollection();
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
        private CertificateRevocationRowCollection _viewDataSouce = new CertificateRevocationRowCollection();


        public CertificateRevocationListView(CertificateRowCollection certificates)
        {
            InitializeComponent();
            ViewDataSouce = new CertificateRevocationRowCollection(certificates);
        }

        private void CertificateRevocationListView_Load(object sender, EventArgs e)
        {
            SetDataSource(ViewDataSouce);
            _isLoaded = true;
        }

        private void SetDataSource(List<CertificateRevocationRow> certificateRows)
        {
            if (certificateRows == null || !certificateRows.Any())
            {
                dataGridViewCertificates.DataSource = null;
                return;
            }

            SortableBindingList<CertificateRevocationRow> boundList = new SortableBindingList<CertificateRevocationRow>(certificateRows);
            boundList.AllowEdit = false;
            boundList.AllowNew = false;
            boundList.AllowRemove = false;

            dataGridViewCertificates.DataSource = null;
            dataGridViewCertificates.DataSource = boundList;

            //dataGridViewCertificates.Columns["LocallySavedFilepath"].Visible = false;
            dataGridViewCertificates.Columns["SourceCertificate"].Visible = false;
            dataGridViewCertificates.Columns["HasErrors"].Visible = false;
            dataGridViewCertificates.Columns["ErrorText"].Visible = false;
            dataGridViewCertificates.Columns["ErrorProperty"].Visible = false;
            dataGridViewCertificates.Columns["DownloadInitiated"].Visible = false;
            dataGridViewCertificates.Columns["InstallInitiated"].Visible = false;

            //dataGridViewCertificates.Columns["DownloadInitiated"].SortMode = DataGridViewColumnSortMode.Automatic;
            dataGridViewCertificates.Columns["DownloadSucceeded"].SortMode = DataGridViewColumnSortMode.Automatic;
            //dataGridViewCertificates.Columns["InstallInitiated"].SortMode = DataGridViewColumnSortMode.Automatic;
            dataGridViewCertificates.Columns["InstallSucceeded"].SortMode = DataGridViewColumnSortMode.Automatic;

            dataGridViewCertificates.Columns["CertificateFriendlyName"].HeaderText = "Friendly Name";
            dataGridViewCertificates.Columns["CrlURL"].HeaderText = "CRL URL";
            dataGridViewCertificates.Columns["RevokedCertCount"].HeaderText = "Cert Count";
            dataGridViewCertificates.Columns["ThisUpdate"].HeaderText = "Updated";
            dataGridViewCertificates.Columns["NextUpdate"].HeaderText = "Next Update";
            dataGridViewCertificates.Columns["LocallySavedFilepath"].HeaderText = "Saved Path";
            //dataGridViewCertificates.Columns["DownloadInitiated"].HeaderText = "Download Started?";
            dataGridViewCertificates.Columns["DownloadSucceeded"].HeaderText = "Downloaded?";
            //dataGridViewCertificates.Columns["InstallInitiated"].HeaderText = "Install Started?";
            dataGridViewCertificates.Columns["InstallSucceeded"].HeaderText = "Installed?";
            
            dataGridViewCertificates.Columns["CertificateFriendlyName"].Width = 150;
            dataGridViewCertificates.Columns["CrlURL"].Width = 300;
            dataGridViewCertificates.Columns["LocallySavedFilepath"].Width = 400;
            //dataGridViewCertificates.Columns["DownloadInitiated"].Width = 120;
            //dataGridViewCertificates.Columns["InstallInitiated"].Width = 100;
            dataGridViewCertificates.Columns["InstallSucceeded"].Width = 70;

            UpdateRowErrorHighlighting();
        }

        private void UpdateRowErrorHighlighting()
        {
            foreach (DataGridViewRow row in dataGridViewCertificates.Rows)
            {
                CertificateRevocationRow certRow = (CertificateRevocationRow)row.DataBoundItem;
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
            dataGridViewCertificates.Refresh();
        }

        private void dataGridViewCertificates_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                if (e.KeyData == Keys.C)
                {
                    CopyCellsToClipboard();
                }
            }
        }

        private void CopyCellsToClipboard()
        {
            // Choose whether to write header. You will want to do this for a CSV file.
            dataGridViewCertificates.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            // Select the cells we want to serialize.
            dataGridViewCertificates.SelectAll();
            // Copy (set clipboard)
            Clipboard.SetDataObject(dataGridViewCertificates.GetClipboardContent());
        }

        private IEnumerable<CertificateRevocationRow> GetSelectedRows()
        {
            var selectedRows = dataGridViewCertificates.SelectedRows.Cast<DataGridViewRow>();
            var selectedCertRows = selectedRows.Select(row => (CertificateRevocationRow)row.DataBoundItem);
            return selectedCertRows;
        }

        private void downloadInstallCRLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEnumerable<CertificateRevocationRow> selectedRows = GetSelectedRows();
            List<CertificateRevocationRow> withoutErrors = selectedRows.Where(row => !row.HasErrors).ToList();

            List<CertificateRevocationRow>  successfullyDownloaded = Utilities.DownloadCRLs(withoutErrors);
            dataGridViewCertificates.Refresh();
            Utilities.InstallCRLs(successfullyDownloaded);
            dataGridViewCertificates.Refresh();
            UpdateRowErrorHighlighting();
        }

        private void downloadCRLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEnumerable<CertificateRevocationRow> selectedRows = GetSelectedRows();
            List<CertificateRevocationRow> withoutErrors = selectedRows.Where(row => !row.HasErrors).ToList();
            List<CertificateRevocationRow>  successfullyDownloaded = Utilities.DownloadCRLs(withoutErrors);
            dataGridViewCertificates.Refresh();
            UpdateRowErrorHighlighting();
        }

        private void installCRLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEnumerable<CertificateRevocationRow> selectedRows = GetSelectedRows();
            IEnumerable<CertificateRevocationRow> withoutErrors = selectedRows.Where(row => !row.HasErrors);
            List<CertificateRevocationRow> successfullyDownloaded = withoutErrors.Where(row => row.DownloadSucceeded).ToList();
            Utilities.InstallCRLs(successfullyDownloaded);
            dataGridViewCertificates.Refresh();
            UpdateRowErrorHighlighting();
        }

        private void contextMenuDataGridRow_Opening(object sender, CancelEventArgs e)
        {
            IEnumerable<CertificateRevocationRow> selectedRows = GetSelectedRows();
            bool anySelectedCRTsSuccessfullyDownloaded = selectedRows.Any(row => row.DownloadSucceeded);
            installCRLToolStripMenuItem.Enabled = anySelectedCRTsSuccessfullyDownloaded;
        }
    }
}
