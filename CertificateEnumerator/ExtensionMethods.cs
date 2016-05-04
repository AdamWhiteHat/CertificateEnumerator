using System.Collections.Generic;

namespace CertificateEnumerator.CertificateExtensionMethods
{
	public static class CertificateExtensionMethodsClass
	{
		public static void VerifyAllCerts(this List<CertificateRow> source)
		{
			if (source != null && source.Count > 0)
			{
				foreach (CertificateRow certRow in source)
				{
					certRow.Verify();
				}
			}
		}
	}
}
