using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CertificateEnumerator
{
	public class CertificateRow : Certificate
	{
		public bool HasErrors  { get; private set; }
		public string ErrorText  { get; private set; }
		public string ErrorProperty { get; private set; }

		public CertificateRow(X509Certificate2 certificate)
			: base(certificate)
		{ }

		public void Validate()
		{
			if (!HasErrors)
			{
				// Validate not in Untrusted store
				if (this.StoreName == "Disallowed")
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
	}
}