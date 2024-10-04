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

        public (List<Tuple<string, string>> successCRLs, List<Tuple<string, string>> failedCRLs) DownloadFiles()
        {
            List<Tuple<string, string>> successCRLs = new List<Tuple<string, string>>();
            List<Tuple<string, string>> failedCRLs = new List<Tuple<string, string>>();

            Utilities.EnsureDirectoryExists(Utilities.DownloadPath);

            using (WebClientWithTimeout client = new WebClientWithTimeout(Utilities.WebRequestTimeout))
            {
                foreach (CertificateRevocationRow certificateRevocationRow in this)
                {
                    bool result = certificateRevocationRow.Download(client);

                    if (result)
                    {
                        successCRLs.Add(new Tuple<string, string>(certificateRevocationRow.CrlURL, certificateRevocationRow.LocallySavedFilepath));
                    }
                    else
                    {
                        failedCRLs.Add(new Tuple<string, string>(certificateRevocationRow.CrlURL, certificateRevocationRow.LocallySavedFilepath));
                    }
                }
            }

            if (failedCRLs.Any())
            {
                string errorFilepath = Path.Combine(Utilities.DownloadPath, Utilities.FailedDownloadsFilename);
                File.AppendAllLines(errorFilepath, Utilities.WrapLinesForLogging(failedCRLs.Select(tup => string.Format("\tURL: {0,-75} DEST: \"{1}\"", $"\"{tup.Item1}\"", tup.Item2))));
            }

            if (successCRLs.Any())
            {
                string successFilepath = Path.Combine(Utilities.DownloadPath, Utilities.SuccessfullyDownloadedFilename);
                File.AppendAllLines(successFilepath, Utilities.WrapLinesForLogging(successCRLs.Select(tup => string.Format("\tURL: {0,-75} DEST: \"{1}\"", $"\"{tup.Item1}\"", tup.Item2))));
            }

            return (successCRLs, failedCRLs);
        }


    }
}
