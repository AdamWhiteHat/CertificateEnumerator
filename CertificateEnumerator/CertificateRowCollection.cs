using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace CertificateEnumerator
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

		public CertificateRowCollection()
			: base()
		{
			foreach (StoreLocation location in ListOfStoreLocation)
			{
				foreach (StoreName name in ListOfStoreNames)
				{
					X509Store store = new X509Store(name, location);
					store.Open(OpenFlags.ReadOnly);

					this.AddRange(CertificateRow.FromStore(store));

					store.Close();
				}
			}			
		}

		private bool _verified = false;
		public void VerifyAllCerts()
		{
			if (this.Count > 0 && !_verified)
			{
				foreach (CertificateRow certRow in this)
				{
					certRow.Validate();
				}
				_verified = true;
			}
		}
	}
}
