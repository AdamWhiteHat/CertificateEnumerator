using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Numerics;
using System.Security.Cryptography;

namespace CertificateManagement
{
	public class CertificateRow
	{
		public bool HasErrors { get; private set; }
		public string ErrorText { get; private set; }
		public string ErrorProperty { get; private set; }
		public bool HasPrivateKey { get; private set; }
		public DateTime EffectiveDate { get; private set; }
		public DateTime ExpirationDate { get; private set; }
		public string StoreLocation { get; internal set; }
		public string StoreName { get; internal set; }
		public string FriendlyName { get; private set; }
		public string Subject { get; private set; }
		public string Issuer { get; private set; }
		public string SerialNumber { get; private set; }
		public string Thumbprint { get; private set; }
		public string KeyAlgorithm { get; private set; }
		public string SignatureAlgorithm { get; private set; }
		public string Hash { get; private set; }
		public string Version { get; private set; }
		public string Format { get; private set; }
		public string PublicKeyType { get; private set; }
		public string PublicKeySize { get; private set; }

		public bool IsChainVerified { get; protected set; }
		public string ChainRevokedStatusInformation { get; protected set; }
		public DateTime ChainVerificationTime { get; private set; }
		public int ChainLength { get; private set; }

		public string ChainApplicationPolicy { get; private set; }

		public string ChainCertificatePolicy { get; private set; }

		public List<string> Extentions { get; private set; }

		internal string[] RawStrings { get; set; }

		internal X509Certificate2 certificate;

		private static readonly string urlMatchStart = "url=http";
		private static readonly string urlMatchEnd = ".crl";

		public CertificateRow(X509Certificate2 cert)
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

			PublicKeyType = certificate.PublicKey.Oid.FriendlyName;
			if (PublicKeyType != "ECC" && certificate.PublicKey.Key != null)
			{
				PublicKeySize = certificate.PublicKey.Key.KeySize.ToString();
			}

			X509Certificate oldCert = (X509Certificate)certificate;
			Hash = oldCert.GetCertHashString();

			Extentions = new List<string>();
			foreach (X509Extension ext in certificate.Extensions)
			{
				Extentions.Add(ext.Format(false));
			}

			RawStrings = GetRawStrings(certificate);
		}

		private static string[] GetRawStrings(X509Certificate2 cert)
		{
			string certificateVerboseString = cert.ToString(true);

			if (string.IsNullOrWhiteSpace(certificateVerboseString)) { return new string[0]; }

			string[] lines = certificateVerboseString.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

			if (lines == null || lines.Length < 1) { return new string[0]; }

			return lines.Select(ln => ln.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToArray();
		}

		public BigInteger GetPublicKeyValue()
		{
			return Utilities.CalculateValue(certificate.GetPublicKey());
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
				IsChainVerified = certificate.Verify();
				if (!IsChainVerified)
				{
					ErrorText = "Errors in the certificate chain.";
					ErrorProperty = "IsChainVerified";
					HasErrors = true;
				}

				using (X509Chain chain = new X509Chain())
				{
					chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
					chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
					chain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;

					bool isValidCert = chain.Build(certificate);
					if (!isValidCert)
					{
						IsChainVerified = false;

						foreach (X509ChainStatus status in chain.ChainStatus)
						{
							if (status.Status != X509ChainStatusFlags.NoError)
							{
								ChainRevokedStatusInformation = status.StatusInformation;
							}
						}

						ErrorText = "Errors in the certificate chain.";
						if (!string.IsNullOrWhiteSpace(ChainRevokedStatusInformation))
						{
							ErrorText = ChainRevokedStatusInformation;
						}
						ErrorProperty = "IsChainVerified";
						HasErrors = true;
					}


					ChainVerificationTime = chain.ChainPolicy.VerificationTime;
					ChainLength = chain.ChainStatus.Length;

					int appPolCount = chain.ChainPolicy.ApplicationPolicy.Count;
					int certPolCount = chain.ChainPolicy.CertificatePolicy.Count;

					StringBuilder stringBuilder = new StringBuilder();
					foreach (Oid oid in chain.ChainPolicy.ApplicationPolicy)
					{
						stringBuilder.Append(oid.FriendlyName);
						stringBuilder.Append(", ");
					}
					ChainApplicationPolicy = stringBuilder.ToString();
					stringBuilder.Clear();

					foreach (Oid oid in chain.ChainPolicy.CertificatePolicy)
					{
						stringBuilder.Append(oid.FriendlyName);
						stringBuilder.Append(", ");
					}
					ChainCertificatePolicy = stringBuilder.ToString();
				}
			}
		}



		public List<string> GetCertificateRevocationListURLs()
		{
			/*
			 * This function looks for CRL URLs in the verbose certificate string representation
			 * That is, this function should extract web addresses that occur after the token "URL="
			 * and that end in ".CRT" from the string returned by calling X509Certificate2.ToString(true)
			 * 
			 * Example:
			 * 
			 * [1]CRL Distribution Point
			 * URL=http://ss.symcb.com/ss.crl
			 * 
			 * -- OR --
			 * 
			 * [1]Authority Info Access
			 * URL=HTTP://cacerts.digicert.com/DigiCertSHA2ExtendedValidationServerCA.CRT
			 * 
			 */

			List<string> urls = RawStrings
								.Where
								(
									ln =>
										ln.StartsWith(urlMatchStart, StringComparison.InvariantCultureIgnoreCase)
										&&
										ln.EndsWith(urlMatchEnd, StringComparison.InvariantCultureIgnoreCase)
								)
								.Distinct()
								.Select(addr => addr.Remove(0, 4))
								.ToList();
			return urls;
		}

		public void Remove()
		{
			StoreName storeName = (StoreName)Enum.Parse(typeof(StoreName), StoreName);
			StoreLocation storeLocation = (StoreLocation)Enum.Parse(typeof(StoreLocation), StoreLocation);

			X509Store store = new X509Store(storeName, storeLocation);
			store.Open(OpenFlags.ReadWrite);
			store.Remove(certificate);
			store.Close();
		}

		public bool ContainsString(string value)
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

		public static bool StoreContainsCertificate(StoreName storeName, StoreLocation storeLocation, X509Certificate2 certificate)
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

