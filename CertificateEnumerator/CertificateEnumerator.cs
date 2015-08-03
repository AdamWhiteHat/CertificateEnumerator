using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace CertificateEnumerator
{
	public partial class Certificates
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


		public List<CertificateRow> GetCertificateRows()
		{
			List<CertificateRow> result = new List<CertificateRow>();

			result.AddRange(CertificateRow.FromStore(new X509Store(StoreName.Disallowed, StoreLocation.LocalMachine)));
			result.AddRange(CertificateRow.FromStore(new X509Store(StoreName.Disallowed, StoreLocation.CurrentUser)));

			foreach (StoreLocation location in ListOfStoreLocation)
			{
				foreach (StoreName name in ListOfStoreNames)
				{
					X509Store store = new X509Store(name, location);
					store.Open(OpenFlags.ReadOnly);

					result.AddRange(CertificateRow.FromStore(store));

					store.Close();
				}
			}
			return result;
		}

		public string GetCertificateString()
		{
			StringBuilder result = new StringBuilder();

			string headerRowFormat = "{0,38}\t{1,-40}\t{2,13}\t{3,-33}\t{4,-60}";
			result.AppendFormat(headerRowFormat,
					"SerialNumber",
					"Thumbprint",
					"StoreLocation",
					"StoreName",
					"FriendlyName");

			result.Append(new String(Enumerable.Repeat('-', 217).ToArray()));

			string storeLocation = string.Empty;
			foreach (StoreLocation location in ListOfStoreLocation)
			{
				if (location == StoreLocation.CurrentUser)
				{
					storeLocation = string.Format("{0,13}","User");
				}
				else if (location == StoreLocation.LocalMachine)
				{
					storeLocation = string.Format("{0,13}", "Machine");
				}

				foreach (StoreName name in ListOfStoreNames)
				{
					X509Store store = new X509Store(name, location);
					store.Open(OpenFlags.ReadOnly);

					foreach (X509Certificate2 mCert in store.Certificates)
					{
						string serialNumber = string.Empty;
						string thumbPrint = string.Empty;
						string storeName = string.Empty;
						string friendlyName = string.Empty;

						friendlyName = mCert.FriendlyName;
						if (string.IsNullOrWhiteSpace(friendlyName))
						{
							friendlyName = string.Format("[{0}]\t[{1}]", mCert.Issuer, mCert.Subject);
						}
						serialNumber = string.Format("{0,38}", mCert.SerialNumber);
						thumbPrint = string.Format("{0,-40}", mCert.Thumbprint);
						storeName = string.Format("{0,-33}", name);
						friendlyName = string.Format("{0,-60}", friendlyName);

						string formatString = string.Empty;
						formatString = "{0}\t{1}\t{2}.{3}\t{4}";

						result.AppendLine
						(
							string.Format
							(
								formatString,
								serialNumber,	// 0
								thumbPrint,		// 1
								storeLocation,	// 2
								storeName,		// 3
								friendlyName	// 4
							)
						);
					}

					store.Close();
				}
			}

			return result.ToString();
		}


	}
}
