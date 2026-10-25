using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CertificateManagement
{
    public class CertificateRevocationRowCollection : List<CertificateRevocationRow>
    {
        internal CertificateRevocationRowCollection()
            : base()
        { }

        public CertificateRevocationRowCollection(CertificateRowCollection certificateRowCollection)
            : base()
        {
            foreach (CertificateRow certificateRow in certificateRowCollection)
            {
                List<string> crlUrls = certificateRow.GetCertificateRevocationListURLs();

                foreach (string crlUrl in crlUrls)
                {
                    base.Add(new CertificateRevocationRow(certificateRow, crlUrl));
                }
            }
        }

        public CertificateRevocationRowCollection(List<CertificateRevocationRow> certificateRevocationRows)
            : base(certificateRevocationRows)
        {
        }

        public List<CertificateRevocationRow> DownloadAllFiles()
        {
            return Utilities.DownloadCRLs(this.ToList());
        }        
    }
}
