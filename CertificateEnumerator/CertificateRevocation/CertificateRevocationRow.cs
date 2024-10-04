using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CertificateManagement
{
    public class CertificateRevocationRow
    {
       // [System.ComponentModel.DataAnnotations.DisplayFormat]

        public CertificateRow SourceCertificate { get; set; }
        public string CertificateFriendlyName { get { return SourceCertificate.FriendlyName; } }
        public string CrlURL { get; private set; }
        public string LocallySavedFilepath { get; private set; }

        public bool HasErrors { get; private set; }
        public string ErrorText { get; private set; }
        public string ErrorProperty { get; private set; }

        public bool DownloadInitiated { get; private set; }
        public bool DownloadSucceeded { get; private set; }
        public bool InstallInitiated { get; private set; }
        public bool InstallSucceeded { get; private set; }


        private CertificateRevocationRow()
        {
            CrlURL = string.Empty;
            ErrorText = string.Empty;
            ErrorProperty = string.Empty;
            HasErrors = false;
            DownloadInitiated = false;
            DownloadSucceeded = false;
            InstallInitiated = false;
            InstallSucceeded = false;
        }

        public CertificateRevocationRow(CertificateRow certificateRow, string crlUrl)
            : this()
        {
            SourceCertificate = certificateRow;
            CrlURL = crlUrl;
        }


        private void SetError(string errorProperty, string errorText)
        {
            HasErrors = true;
            ErrorProperty = errorProperty;
            ErrorText = errorText;
        }

        public bool Download(WebClientWithTimeout webClient)
        {
            LocallySavedFilepath = string.Empty;
            DownloadInitiated = true;


            try
            {
                Uri uri = new Uri(CrlURL);
                if (uri == null)
                {
                    SetError(nameof(CrlURL), $"System.Uri could not parse the URL: \"{CrlURL}\"");
                    return false;
                }
                string filename = uri.LocalPath.TrimStart('/', '\\');

                int index = filename.LastIndexOf('/');
                if (index != -1)
                {
                    filename = filename.Substring(index + 1);
                }

                if (string.IsNullOrWhiteSpace(filename))
                {
                    SetError(nameof(CrlURL), "Could not extract a filename from the URL to use as a local filename for downloading.");
                    return false;
                }
                LocallySavedFilepath = Path.Combine(Utilities.DownloadPath, filename);

                // TODO: Make async                      
                webClient.DownloadFile(CrlURL, LocallySavedFilepath);

                if (File.Exists(LocallySavedFilepath))
                {
                    DownloadSucceeded = true;
                    return true;
                }
                else
                {
                    SetError(nameof(DownloadSucceeded), "Could not download CRL to local destination.");
                    DownloadSucceeded = false;
                    return false;
                }
            }
            catch (WebException ex)
            {
                SetError(nameof(DownloadSucceeded), "Could not download CRL. WebException.Message: " + ex.Message);
                DownloadSucceeded = false;
                return false;
            }

        }








    }
}
