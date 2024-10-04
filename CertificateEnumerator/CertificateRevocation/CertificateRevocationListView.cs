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

            dataGridViewCertificates.Columns["SourceCertificate"].Visible = false;
            //dataGridViewCertificates.Columns["CertificateRevocationListURL"].Width = 420;
            dataGridViewCertificates.Columns["CrlURL"].Name = "CRL URL";


        }

        private void DownloadCRLs()
        {
            (List<Tuple<string, string>> successCRLs, List<Tuple<string, string>> failedCRLs) results = ViewDataSouce.DownloadFiles();

            List<Tuple<string, string>> successCRLs = results.successCRLs;
            List<Tuple<string, string>> failedCRLs = results.failedCRLs;

            // TODO: Do something with successCRLs
        }

        private void InstallCRLs()
        {
            List<string> toInstallCRLs = Directory.EnumerateFiles(Utilities.DownloadPath, "*.crl").ToList();
            List<string> installedCRLs = Utilities.InstallCertificateRevocationLists(toInstallCRLs);
        }

    }
}
