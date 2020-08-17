﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.IdentityModel.Selectors;

namespace CertificateEnumeratorGUI
{
	public class Certificate
	{
		public bool IsVerified { get; protected set; }
		public bool HasPrivateKey { get; private set; }
		public bool HasCrlDistributionPoint { get; private set; }
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
		public string Version { get; private set; }
		public string Format { get; private set; }
		public List<string> Extentions { get; private set; }
		public List<string> CrlDistributionPointURLs { get; private set; }

		protected X509Certificate2 certificate;

		private static string urlMatchStart = "url=http";
		private static string urlMatchEnd = ".crl";

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

			CrlDistributionPointURLs = GetCertificateRevocationListURLs();
			HasCrlDistributionPoint = CrlDistributionPointURLs.Any();
		}

		public bool Verify()
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

			string certificateVerboseString = certificate.ToString(true);

			if (string.IsNullOrWhiteSpace(certificateVerboseString)) { return new List<string>(); }

			string[] lines = certificateVerboseString.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

			if (lines == null || lines.Length < 1) { return new List<string>(); }

			lines = lines.Select(ln => ln.Trim()).Distinct().ToArray();

			List<string> urls = lines
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
	}
}

