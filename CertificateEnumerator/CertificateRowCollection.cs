using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace CertificateEnumerator
{
    public partial class CertificateRowCollection : List<CertificateRow>
    {
        public static List<StoreLocation> ListOfStoreLocation =
            new List<StoreLocation>()
            {
                StoreLocation.CurrentUser,
                StoreLocation.LocalMachine
            };

        public static List<StoreName> ListOfStoreNames =
            new List<StoreName>()
            {
                StoreName.Disallowed,
                StoreName.AddressBook,
                StoreName.AuthRoot,
                StoreName.CertificateAuthority,
                StoreName.My,
                StoreName.Root,
                StoreName.TrustedPeople,
                StoreName.TrustedPublisher
            };

        public static CertificateRowCollection FromFolder(List<X509Certificate2> store)
        {
            List<CertificateRow> results = new List<CertificateRow>();
            foreach (X509Certificate2 cert in store)
            {
                CertificateRow newRow = new CertificateRow(cert);
                newRow.StoreName = "(file)";
                newRow.StoreLocation = "(none)";
                results.Add(newRow);
            }
            return new CertificateRowCollection(results);
        }

        public CertificateRowCollection()
            : base()
        {
            foreach (StoreLocation location in ListOfStoreLocation)
            {
                foreach (StoreName name in ListOfStoreNames)
                {
                    X509Store store = new X509Store(name, location);
                    store.Open(OpenFlags.ReadOnly);

                    this.AddRange(CertificateRow.FromStore(store));

                    store.Close();
                }
            }
        }

        public CertificateRowCollection(List<CertificateRow> certs)
           : base(certs)
        {
        }

        public List<string> GetAllCertificatesRevocationListURLs()
        {
            return this.SelectMany(crt => crt.GetCertificateRevocationListURLs()).ToList();
        }

        public List<string> DownloadAllCertificatesRevocationListURLs()
        {
            List<string> crlUrls = GetAllCertificatesRevocationListURLs();
            return Utilities.DownloadFiles(crlUrls);
        }

        public List<string> InstallCertificatesRevocationLists(List<string> crlFilenames)
        {
            List<string> installedCRLs = new List<string>();
            List<string> erroredCRLs = new List<string>();
            foreach (string file in crlFilenames)
            {
                if (!File.Exists(file))
                {
                    erroredCRLs.Add(file);
                    continue;
                }

                if (Utilities.InstallCertificate(file))
                {
                    installedCRLs.Add(file);
                }
                else
                {
                    erroredCRLs.Add(file);
                }
            }

            return installedCRLs;
        }
               
        public List<string> GetAllCertificatesPublicKeys()
        {
            Dictionary<string, string> certificateDictionary = new Dictionary<string, string>();
            foreach (CertificateRow cert in this)
            {
                string thumbprint = cert.Thumbprint;
                if (!certificateDictionary.ContainsKey(thumbprint))
                {
                    certificateDictionary.Add(thumbprint, cert.GetPublicKey());
                }
            }

            return certificateDictionary.Values.Select(val => val).ToList();         
        }

        private bool _verified = false;
        public void VerifyAllCerts()
        {
            if (this.Count > 0 && !_verified)
            {
                foreach (CertificateRow row in this)
                {
                    row.Validate();
                }
                _verified = true;
            }
        }
    }
}
