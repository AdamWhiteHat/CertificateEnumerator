using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace CertificateManagement
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

        public CertificateRowCollection(List<CertificateRow> certs)
            : base(certs)
        {
        }

        public CertificateRowCollection(List<X509Certificate2> certs)
            : base()
        {
            foreach (X509Certificate2 cert in certs)
            {
                CertificateRow newRow = new CertificateRow(cert);
                newRow.StoreName = "(none)";
                newRow.StoreLocation = "(none)";
                base.Add(newRow);
            }
        }

        public CertificateRowCollection(X509Certificate2Collection certs)
            : this(certs.Cast<X509Certificate2>().ToList())
        {
        }

        public CertificateRowCollection(X509Store store)
           : this(store.Certificates)
        {
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

                    foreach (X509Certificate2 cert in store.Certificates)
                    {
                        CertificateRow newRow = new CertificateRow(cert);
                        newRow.StoreName = store.Name;
                        newRow.StoreLocation = store.Location.ToString();
                        base.Add(newRow);
                    }

                    store.Close();
                }
            }
        }

        public List<string> GetAllCertificatesRevocationListURLs()
        {
            return this.SelectMany(crt => crt.GetCertificateRevocationListURLs()).Distinct().ToList();
        }

        public List<string> GetAllCertificatesPublicKeyValues()
        {
            Dictionary<string, string> certificateDictionary = new Dictionary<string, string>();
            foreach (CertificateRow cert in this)
            {
                string thumbprint = cert.Thumbprint;
                if (!certificateDictionary.ContainsKey(thumbprint))
                {
                    certificateDictionary.Add(thumbprint, cert.GetPublicKeyValue().ToString());
                }
            }

            return certificateDictionary.Values.Select(val => val).ToList();
        }

        private bool _verified = false;
        public void VerifyAllCerts()
        {
            if (!_verified)
            {
                if (this.Count > 0 && !_verified)
                {
                    foreach (CertificateRow row in this)
                    {
                        row.Validate();
                    }
                }
                _verified = true;
            }
        }
    }
}
