using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Numerics;
using System.IO;

namespace CertificateEnumeratorGUI
{
    public class CertificateRow : Certificate
    {
        public bool HasErrors { get; private set; }
        public string ErrorText { get; private set; }
        public string ErrorProperty { get; private set; }

        private static string urlMatchStart = "url=http";
        private static string urlMatchEnd = ".crl";

        public CertificateRow(X509Certificate2 certificate)
            : base(certificate)
        { }

        public string GetPublicKey()
        {
            return Utilities.CalculateValue(certificate.GetPublicKey()).ToString();
        }
               
        public List<string> GetCertificateRevocationListURLs()
        {
            // [1]CRL Distribution Point
            // URL=http://ss.symcb.com/ss.crl
            /* - or - */
            // [1]Authority Info Access
            // URL=HTTP://cacerts.digicert.com/DigiCertSHA2ExtendedValidationServerCA.CRT
                        
            string certificateVerboseString = certificate.ToString(true);

            if (string.IsNullOrWhiteSpace(certificateVerboseString)) { return new List<string>(); }

            string[] lines = certificateVerboseString.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (lines == null || lines.Length < 1) { return new List<string>(); }

            lines = lines.Select(ln => ln.ToLowerInvariant().Trim()).Distinct().ToArray();

            List<string> urls = lines.Where(ln => ln.StartsWith(urlMatchStart) && ln.EndsWith(urlMatchEnd))
                                .Distinct()
                                .Select(addr => addr.Replace("url=", ""))
                                .ToList();
            return urls;
        }

        public void Validate()
        {
            if (!HasErrors)
            {
				// Validate not in Untrusted store
				bool disallowed = false;

				disallowed = (this.StoreName == "Disallowed");

				if (!disallowed)
				{
					X509Store machineStore = new X509Store(System.Security.Cryptography.X509Certificates.StoreName.Disallowed, System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine);
					X509Store userStore = new X509Store(System.Security.Cryptography.X509Certificates.StoreName.Disallowed, System.Security.Cryptography.X509Certificates.StoreLocation.CurrentUser);

					disallowed |= machineStore.Certificates.Contains(this.certificate);
					disallowed |= userStore.Certificates.Contains(this.certificate);

					machineStore.Close();
					userStore.Close();
				}

                if (disallowed)
                {
                    ErrorText = "Certificate in Untrusted store.";
                    ErrorProperty = "StoreName";
                    HasErrors = true;
                }

                // Validate EffectiveDate & ExpirationDate
                else if (DateTime.Now.CompareTo(this.EffectiveDate) < 0)
                {
                    ErrorText = "Certificate not in effect.";
                    ErrorProperty = "EffectiveDate";
                    HasErrors = true;
                }
                else if (DateTime.Now.CompareTo(this.ExpirationDate) > 0)
                {
                    ErrorText = "Certificate expired.";
                    ErrorProperty = "ExpirationDate";
                    HasErrors = true;
                }

                // Lastly, verify cert chain				
                else if (!base.Verify())
                {
                    ErrorText = "Errors in the certificate chain.";
                    ErrorProperty = "IsVerified";
                    HasErrors = true;
                }
            }
        }

		static bool StoreContainsCertificate(StoreName storeName, StoreLocation storeLocation, X509Certificate2 certificate)
		{
			X509Store store = new X509Store(storeName, storeLocation);
			X509Certificate2Collection certificates = null;
			try
			{
				store.Open(OpenFlags.ReadOnly);

				certificates = store.Certificates.Find(X509FindType.FindByThumbprint, certificate.GetCertHash(), false);
				return certificates.Count > 0;
			}
			finally
			{
				//SecurityUtils.ResetAllCertificates(certificates);
				store.Close();				
			}
		}
	}
}