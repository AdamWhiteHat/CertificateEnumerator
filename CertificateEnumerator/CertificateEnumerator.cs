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
	}
}
