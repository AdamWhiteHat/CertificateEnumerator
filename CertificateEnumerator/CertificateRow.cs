using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace CertificateEnumerator
{
	public class CertificateRow
	{
		public bool PrivateKey { get; set; }
		public bool IsVerified { get; set; }
		public string StoreLocation { get; set; }
		public string StoreName { get; set; }
		public string FriendlyName { get; set; }
		public string SerialNumber { get; set; }
		public string Thumbprint { get; set; }
		public string Algorithm { get; set; }
		private X509Certificate2 _cert;

		public CertificateRow(X509Certificate2 certificate)
		{
			_cert = certificate;
			Algorithm = _cert.GetKeyAlgorithm();
			PrivateKey = _cert.HasPrivateKey;
			SerialNumber = _cert.SerialNumber;
			Thumbprint = _cert.Thumbprint;
			FriendlyName = _cert.FriendlyName;
			if (string.IsNullOrWhiteSpace(FriendlyName))
			{
				FriendlyName = string.Format("[{0}]\t[{1}]", _cert.Issuer, _cert.Subject);
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

		public void Verify()
		{
			if (_cert != null)
			{
				IsVerified = _cert.Verify();
			}
		}
	}
}