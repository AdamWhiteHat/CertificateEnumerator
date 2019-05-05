using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace CertificateEnumerator
{
    public class Certificate
    {
        public bool IsVerified { get; protected set; }
        public DateTime EffectiveDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public string StoreLocation { get; internal set; }
        public string StoreName { get; internal set; }
        public string FriendlyName { get; private set; }
        public string Subject { get; private set; }
        public string Issuer { get; private set; }
        public string SerialNumber { get; private set; }
        public string Thumbprint { get; private set; }
        public bool HasPrivateKey { get; private set; }
        public string KeyAlgorithm { get; private set; }
        public string SignatureAlgorithm { get; private set; }
        public string Version { get; private set; }
        public string Format { get; private set; }
        public List<string> Extentions { get; private set; }

        protected X509Certificate2 certificate;

        public Certificate(X509Certificate2 cert)
        {
            if (cert == null)
            {
                throw new ArgumentNullException("cert");
            }

            certificate = cert;

            KeyAlgorithm = certificate.GetKeyAlgorithm();
            HasPrivateKey = certificate.HasPrivateKey;
            SerialNumber = certificate.SerialNumber;
            Thumbprint = certificate.Thumbprint;
            FriendlyName = certificate.FriendlyName;// !string.IsNullOrWhiteSpace(_certificate.FriendlyName) ? _certificate.FriendlyName : string.Format("[{0}]\t[{1}]", _certificate.Issuer, _certificate.Subject);
            Subject = certificate.Subject;
            EffectiveDate = certificate.NotBefore;
            ExpirationDate = certificate.NotAfter;
            Format = certificate.GetFormat();
            Issuer = certificate.Issuer;
            SignatureAlgorithm = certificate.SignatureAlgorithm.FriendlyName;
            Version = certificate.Version.ToString();

            Extentions = new List<string>();
            foreach (X509Extension ext in certificate.Extensions)
            {
                Extentions.Add(ext.Format(false));
            }
        }

        public static List<CertificateRow> FromStore(X509Store store)
        {
            List<CertificateRow> results = new List<CertificateRow>();
            foreach (X509Certificate2 cert in store.Certificates)
            {
                CertificateRow newRow = new CertificateRow(cert);
                newRow.StoreName = store.Name;
                newRow.StoreLocation = store.Location.ToString();
                results.Add(newRow);
            }
            return results;
        }       

        protected bool Verify()
        {
            if (certificate == null)
            {
                return false;
            }
            else
            {
                IsVerified = certificate.Verify();
                return IsVerified;
            }
        }

        public bool Contains(string value)
        {
            return
            (
                   FriendlyName.Contains(value)
                || Subject.Contains(value)
                || Issuer.Contains(value)
                || SerialNumber.Contains(value)
                || Thumbprint.Contains(value)
                || KeyAlgorithm.Contains(value)
                || SignatureAlgorithm.Contains(value)
                || Version.Contains(value)
                || Format.Contains(value)
                || Extentions.Contains(value)
                || StoreLocation.Contains(value)
                || StoreName.Contains(value)
                || EffectiveDate.ToString().Contains(value)
                || ExpirationDate.ToString().Contains(value)
            );
        }
    }
}

