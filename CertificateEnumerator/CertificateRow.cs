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
		public bool IsVerified { get; private set; }
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
				
		private X509Certificate2 _certificate;

		public CertificateRow(X509Certificate2 cert)
		{
			_certificate = cert;

			KeyAlgorithm = _certificate.GetKeyAlgorithm();
			HasPrivateKey = _certificate.HasPrivateKey;
			SerialNumber = _certificate.SerialNumber;
			Thumbprint = _certificate.Thumbprint;
			FriendlyName = _certificate.FriendlyName;// !string.IsNullOrWhiteSpace(_certificate.FriendlyName)
				// ? _certificate.FriendlyName
				// : string.Format("[{0}]\t[{1}]", _certificate.Issuer, _certificate.Subject);
			Subject = _certificate.Subject;
			EffectiveDate = _certificate.NotBefore;
			ExpirationDate = _certificate.NotAfter;
			Format = _certificate.GetFormat();
			Issuer = _certificate.Issuer;		
			SignatureAlgorithm = _certificate.SignatureAlgorithm.FriendlyName;
			Version = _certificate.Version.ToString();

			Extentions = new List<string>();
			foreach (X509Extension ext in _certificate.Extensions)
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

		public void Verify()
		{
			if (_certificate != null)
			{
				IsVerified = _certificate.Verify();
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