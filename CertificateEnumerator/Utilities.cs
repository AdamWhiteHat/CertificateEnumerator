using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Numerics;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace CertificateEnumeratorGUI
{
	public static class Utilities
	{
		public static string ToDownloadFilename = "CRLs_ToDownload.txt";

		public static string SuccessfullyDownloadedFilename = "CRLs_SuccessfullyDownloaded.txt";
		public static string FailedDownloadsFilename = "CRLs_FailedDownloads.txt";

		public static string SuccessfullyInstalledFilename = "CRLs_SuccessfullyInstalled.txt";
		public static string FailedInstallsFilename = "CRLs_FailedInstalls.txt";

		public static string PublicKeysStoreOutputFilename = "PublicKeys.Store.Output.txt";
		public static string PublicKeysFolderOutputFilename = "PublicKeys.Folder.Output.txt";

		public static string DownloadPath = Path.GetFullPath(Settings.CRL_DownloadPath);
		public static string CertUtilExecutable = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "CertUtil.exe");

		private static string dashedLine = "---";
		private static BigInteger ByteMax = new BigInteger(256);
		private static int webRequestTimeout = 2000;

		public static List<string> GetTimestampLogfileHeader()
		{
			DateTime now = DateTime.Now;

			return new List<string>() { Environment.NewLine, $"{dashedLine} {now.ToLongDateString()} @ {now.ToLongTimeString()}" };
		}

		public static List<string> WrapLinesForLogging(IEnumerable<string> lines)
		{
			List<string> results = GetTimestampLogfileHeader(); // Header

			results.AddRange(lines); // Body

			results.Add(dashedLine); // Footer

			return results;
		}

		public static void EnsureDirectoryExists(string path)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}

		public static string EnsureFilenameNotExists(string filename)
		{
			int counter = 1;
			string result = filename;
			string extension = Path.GetExtension(result);
			while (File.Exists(result))
			{
				result = Path.ChangeExtension(result, string.Concat(".", counter.ToString().PadLeft(3, '0'), extension));
				counter++;
			}
			return result;
		}

		public static List<X509Certificate2> SearchForCertsInFolder(string path)
		{
			if (string.IsNullOrWhiteSpace(path)) { throw new ArgumentException("Argument path must not be null, empty or whitespace", "path"); }
			if (!Directory.Exists(path)) { throw new DirectoryNotFoundException("Path must exist: " + path); }

			IEnumerable<string> filePaths = Directory.EnumerateFiles(path, "*.cer", SearchOption.TopDirectoryOnly);

			List<X509Certificate2> results = new List<X509Certificate2>();
			foreach (string file in filePaths)
			{
				X509Certificate2 cert = new X509Certificate2(file);
				results.Add(cert);
			}
			return results;
		}

		public static List<string> GetCRLsToDownloadURLs()
		{
			string todownloadFilepath = Path.Combine(Utilities.DownloadPath, Utilities.ToDownloadFilename);
			if (File.Exists(todownloadFilepath))
			{
				IEnumerable<string> additionalUrls = File.ReadAllLines(todownloadFilepath).Select(line => line.Trim()).Where(line => !string.IsNullOrWhiteSpace(line));

				if (additionalUrls.Any())
				{
					return additionalUrls.ToList();
				}
			}

			return new List<string>();
		}

		public static List<Tuple<string, string>> DownloadFiles(List<string> remoteFileURIs)
		{
			List<Tuple<string, string>> successCRLs = new List<Tuple<string, string>>();
			List<Tuple<string, string>> failedCRLs = new List<Tuple<string, string>>();

			EnsureDirectoryExists(DownloadPath);

			using (WebClientWithTimeout client = new WebClientWithTimeout(webRequestTimeout))
			{
				foreach (string remoteFile in remoteFileURIs)
				{
					string localFile = string.Empty;
					try
					{
						Uri uri = new Uri(remoteFile);
						if (uri == null) { continue; }
						string filename = uri.LocalPath.TrimStart('/', '\\');

						int index = filename.LastIndexOf('/');
						if (index != -1)
						{
							filename = filename.Substring(index + 1);
						}

						if (string.IsNullOrWhiteSpace(filename)) { continue; }
						localFile = Path.Combine(DownloadPath, filename);

						// TODO: Make async                      
						client.DownloadFile(remoteFile, localFile);

						if (File.Exists(localFile))
						{
							successCRLs.Add(new Tuple<string, string>(remoteFile, localFile));
						}
						else
						{
							failedCRLs.Add(new Tuple<string, string>(remoteFile, localFile));
						}
					}
					catch (WebException)
					{
						failedCRLs.Add(new Tuple<string, string>(remoteFile, localFile));
					}
				}
			}

			if (failedCRLs.Any())
			{
				string errorFilepath = Path.Combine(Utilities.DownloadPath, Utilities.FailedDownloadsFilename);
				File.AppendAllLines(errorFilepath, WrapLinesForLogging(failedCRLs.Select(tup => string.Format("\tURL: {0,-75} DEST: \"{1}\"", $"\"{tup.Item1}\"", tup.Item2))));
			}

			if (successCRLs.Any())
			{
				string successFilepath = Path.Combine(Utilities.DownloadPath, Utilities.SuccessfullyDownloadedFilename);
				File.AppendAllLines(successFilepath, WrapLinesForLogging(successCRLs.Select(tup => string.Format("\tURL: {0,-75} DEST: \"{1}\"", $"\"{tup.Item1}\"", tup.Item2))));
			}

			return successCRLs;
		}

		public static List<string> InstallCertificateRevocationLists(List<string> crlFilenames)
		{
			List<string> installedCRLs = new List<string>();
			List<string> faildCRLs = new List<string>();

			foreach (string file in crlFilenames)
			{
				if (!File.Exists(file))
				{
					faildCRLs.Add(file);
					continue;
				}

				if (Utilities.InstallCRL(file))
				{
					installedCRLs.Add(file);
				}
				else
				{
					faildCRLs.Add(file);
				}
			}

			if (faildCRLs.Any())
			{
				string failedFilepath = Path.Combine(Utilities.DownloadPath, Utilities.FailedInstallsFilename);
				File.AppendAllLines(failedFilepath, WrapLinesForLogging(faildCRLs));
			}

			if (installedCRLs.Any())
			{
				string successfulFilepath = Path.Combine(Utilities.DownloadPath, Utilities.SuccessfullyInstalledFilename);
				File.AppendAllLines(successfulFilepath, WrapLinesForLogging(installedCRLs));
			}

			return installedCRLs;
		}

		private static bool InstallCRL(string crlFilepath)
		{
			if (!File.Exists(crlFilepath) || !File.Exists(CertUtilExecutable))
			{
				return false;
			}

			try
			{
				bool result = ExecuteCommandLine(CertUtilExecutable, $"-addstore -f Disallowed \"{crlFilepath}\"");
				return result;
			}
			catch (Exception ex)
			{
				SettingsReader.LogException(ex);
				string exceptionType = ex.GetType().ToString();
				string message = ex.ToString();
				MessageBox.Show(message, exceptionType, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}

		public static bool ExecuteCommandLine(string executablePath, string arguments)
		{
			bool result = false;
			using (Process process = new Process())
			{
				process.StartInfo = new ProcessStartInfo(executablePath, arguments)
				{
					WindowStyle = ProcessWindowStyle.Hidden
				};
				result = process.Start();
				process.WaitForExit();
				process.Close();
			}
			return result;
		}

		internal static BigInteger CalculateValue(byte[] input)
		{
			byte[] localCopy = new List<byte>(input).ToArray();
			Array.Reverse(localCopy);

			int counter = 0;
			BigInteger placeValue = new BigInteger(0);
			BigInteger result = new BigInteger(0);
			foreach (byte octet in localCopy)
			{
				placeValue = BigInteger.Pow(ByteMax, counter);
				placeValue *= octet;
				result += placeValue;
				counter++;
			}
			return result;
		}
	}
}
