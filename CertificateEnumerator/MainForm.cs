using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CertificateManagement
{
    public partial class MainForm : Form
    {
        private CertificateRowCollection certificateStoreCollection;
        private CertificateStoreView certificateStoreView;
        private CertificateRevocationListView certificateRevocationView;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Thread myThread = new Thread(new ThreadStart(() => certificateStoreCollection = new CertificateRowCollection()));
            myThread.Start();
        }

        private void btnShowCertStore_Click(object sender, EventArgs e)
        {
            certificateStoreView = new CertificateStoreView(certificateStoreCollection);
            certificateStoreView.ShowDialog();
        }

        private void btnShowCertRevocationList_Click(object sender, EventArgs e)
        {
            certificateRevocationView = new CertificateRevocationListView(certificateStoreCollection);
            certificateRevocationView.ShowDialog();
        }

    }
}
