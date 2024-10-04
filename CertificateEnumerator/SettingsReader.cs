using System;
using System.IO;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;

namespace CertificateManagement
{
	public static class SettingsReader
	{
		public static string ExceptionsLogFilename = "Exceptions.log.txt";

		public static T GetSettingValue<T>(string SettingName)
		{
			try
			{
				if (SettingExists(SettingName))
				{
					T result = (T)Convert.ChangeType(ConfigurationManager.AppSettings[SettingName], typeof(T));
					if (result != null)
					{
						return result;
					}
				}
			}
			catch (Exception ex)
			{
				LogException(ex);
			}

			return default(T);
		}

		public static bool SettingExists(string SettingName)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(SettingName))
				{
					return false;
				}
				else if (!ConfigurationManager.AppSettings.AllKeys.Contains(SettingName))
				{
					return false;
				}
				else if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[SettingName]))
				{
					return false;
				}

				return true;
			}
			catch (Exception ex)
			{
				LogException(ex);
				return false;
			}
		}

		public static void LogException(Exception ex)
		{
			string message = ex.ToString();
			File.AppendAllText(ExceptionsLogFilename, message + Environment.NewLine);
		}
	}
}
