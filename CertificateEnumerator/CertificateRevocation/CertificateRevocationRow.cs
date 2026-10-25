using CertificateEnumerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CertificateManagement
{
    public class CertificateRevocationRow
    {
        // [System.ComponentModel.DataAnnotations.DisplayFormat]

        public event EventHandler<CompletedEventArgs> DownloadCompleted;
        public event EventHandler<CompletedEventArgs> InstallCompleted;


        public CertificateRow SourceCertificate { get; set; }
        public string CertificateFriendlyName { get { return SourceCertificate.FriendlyName; } }
        public string CrlURL { get; private set; }
        public string LocallySavedFilepath { get; private set; }

        public int? RevokedCertCount { get; private set; }
        public string ThisUpdate { get; private set; }
        public string NextUpdate { get; private set; }

        public bool HasErrors { get; private set; }
        public string ErrorText { get; private set; }
        public string ErrorProperty { get; private set; }

        public bool DownloadInitiated { get; private set; }
        public bool DownloadSucceeded { get; private set; }
        public bool InstallInitiated { get; private set; }
        public bool InstallSucceeded { get; private set; }

        private static Org.BouncyCastle.X509.X509CrlParser _crlParser = new Org.BouncyCastle.X509.X509CrlParser(true);


        private CertificateRevocationRow()
        {
            SourceCertificate = null;
            CrlURL = string.Empty;
            LocallySavedFilepath = string.Empty;
            RevokedCertCount = null;
            ThisUpdate = null;
            NextUpdate = null;
            HasErrors = false;
            ErrorText = string.Empty;
            ErrorProperty = string.Empty;
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

        public void Open()
        {
            if (!string.IsNullOrWhiteSpace(LocallySavedFilepath) && File.Exists(LocallySavedFilepath))
            {
                X509ContentType type = X509Certificate2.GetCertContentType(LocallySavedFilepath);

                X509Certificate2 cert2 = null;
                try
                {
                    cert2 = new X509Certificate2(LocallySavedFilepath);
                }
                catch
                { }

                if (cert2 != null)
                {
                    X509Certificate2UI.DisplayCertificate(cert2);
                }
            }
            else
            {
                X509Certificate2UI.DisplayCertificate(SourceCertificate.certificate);
            }
        }

        public void RaiseDownloadCompleted(bool success)
        {
            DownloadCompleted?.Invoke(this, new CompletedEventArgs(success));
        }

        public void RaiseInstallCompleted(bool success)
        {
            InstallCompleted?.Invoke(this, new CompletedEventArgs(success));
        }

        public void SetError(string errorProperty, string errorText)
        {
            HasErrors = true;
            ErrorProperty = errorProperty;
            ErrorText = errorText;
        }

        private void CertificateRevocationRow_DownloadCompleted(object sender, CompletedEventArgs e)
        {
            DownloadCompleted -= CertificateRevocationRow_DownloadCompleted;
            if (e.Success && File.Exists(LocallySavedFilepath))
            {
                ParseCRL(LocallySavedFilepath);
            }
        }

        private void ParseCRL(string fullPath)
        {
            Org.BouncyCastle.X509.X509Crl crl = _crlParser.ReadCrl(File.ReadAllBytes(fullPath));
            if (crl == null)
            {
                return;
            }
            ISet<Org.BouncyCastle.X509.X509CrlEntry> revokedCerts = crl.GetRevokedCertificates();
            if (revokedCerts == null || !revokedCerts.Any())
            {
                return;
            }

            RevokedCertCount = revokedCerts.Count;
            ThisUpdate = crl.ThisUpdate.ToString("d");
            NextUpdate = crl.NextUpdate.Value.ToString("d");
        }

        public bool Download(WebClientWithTimeout webClient)
        {
            LocallySavedFilepath = string.Empty;
            DownloadInitiated = true;

            DownloadCompleted += CertificateRevocationRow_DownloadCompleted;

            try
            {
                Uri uri = new Uri(CrlURL);
                if (uri == null)
                {
                    SetError(nameof(CrlURL), $"System.Uri could not parse the URL: \"{CrlURL}\"");
                    RaiseDownloadCompleted(false);
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
                    RaiseDownloadCompleted(false);
                    return false;
                }

                string uriHost = uri.Host.Replace(".", "-");
                if (!string.IsNullOrWhiteSpace(uriHost))
                {
                    filename = $"{uriHost}_{filename}";
                }

                LocallySavedFilepath = Path.Combine(Utilities.DownloadPath, filename);

                // TODO: Make async                      
                webClient.DownloadFile(CrlURL, LocallySavedFilepath);

                if (File.Exists(LocallySavedFilepath))
                {
                    DownloadSucceeded = true;
                    RaiseDownloadCompleted(DownloadSucceeded);
                    return true;
                }
                else
                {
                    DownloadSucceeded = false;
                    SetError(nameof(DownloadSucceeded), "Could not download CRL to local destination.");
                    RaiseDownloadCompleted(DownloadSucceeded);
                    return false;
                }
            }
            catch (WebException ex)
            {
                DownloadSucceeded = false;
                SetError(nameof(DownloadSucceeded), "Could not download CRL. WebException.Message: " + ex.Message);
                RaiseDownloadCompleted(DownloadSucceeded);
                return false;
            }
        }

        public bool Install()
        {
            if (!File.Exists(Utilities.CertUtilExecutable))
            {
                RaiseInstallCompleted(false);
                return false;
            }

            InstallInitiated = true;

            if (!DownloadSucceeded)
            {
                InstallSucceeded = false;
                SetError(nameof(InstallSucceeded), $"{nameof(DownloadSucceeded)} = false");
                RaiseInstallCompleted(InstallSucceeded);
                return false;
            }

            if (!File.Exists(LocallySavedFilepath))
            {
                InstallSucceeded = false;
                SetError(nameof(InstallSucceeded), $"Downloaded CRL file no longer exists: \"{LocallySavedFilepath}\"");
                RaiseInstallCompleted(InstallSucceeded);
                return false;
            }

            bool result = false;
            try
            {
                result = Utilities.ExecuteCommandLine(Utilities.CertUtilExecutable, $"-addstore -f Disallowed \"{LocallySavedFilepath}\"");
            }
            catch (Exception ex)
            {
                SettingsReader.LogException(ex);
                string exceptionType = ex.GetType().ToString();
                string message = ex.ToString();
                SetError(nameof(InstallSucceeded), "Exception caught trying to run CertUtil.exe; Exception.Message: " + ex.Message);
                result = false;
            }

            InstallSucceeded = result;
            RaiseInstallCompleted(InstallSucceeded);
            return result;
        }
    }
}
