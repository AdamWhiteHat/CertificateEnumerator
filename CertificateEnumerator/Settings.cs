using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertificateManagement
{
	public static class Settings
	{
		public static string CRL_DownloadPath = SettingsReader.GetSettingValue<string>("CRL.DownloadPath");
	}
}
