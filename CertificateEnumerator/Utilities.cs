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

namespace CertificateEnumerator
{
	public static class Utilities
	{
		public static string ToDownloadFilename = "CRLs_ToDownload.txt";
		public static string FailedDownloadsFilename = "CRLs_FailedDownloads.txt";
		public static string SuccessfullyInstalledFilename = "CRLs_SuccessfullyInstalled.txt";

		public static string DownloadPath = @"C:\Temp\Certificates\Downloads"; //Path.GetFullPath("Downloads"); // Path.GetTempPath() 
		public static string CertUtilExecutable = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "CertUtil.exe");

		private static BigInteger ByteMax = new BigInteger(256);
		private static int webRequestTimeout = 2000;

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

		public static List<X509Certificate2> CertsFromFolder(string path)
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

		public static List<string> DownloadFiles(List<string> remoteFileURIs)
		{
			List<string> successCRLs = new List<string>();
			List<Tuple<string, string>> erroredCRLs = new List<Tuple<string, string>>();

			if (!Directory.Exists(DownloadPath))
			{
				Directory.CreateDirectory(DownloadPath);
			}

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
							successCRLs.Add(localFile);
						}
						else
						{
							erroredCRLs.Add(new Tuple<string, string>(remoteFile, localFile));
						}
					}
					catch (WebException)
					{
						erroredCRLs.Add(new Tuple<string, string>(remoteFile, localFile));
					}
				}
			}

			if (erroredCRLs.Any())
			{
				string errorFile = Path.Combine(Utilities.DownloadPath, Utilities.FailedDownloadsFilename);
				File.WriteAllLines(errorFile, erroredCRLs.Select(tup => $"{{\n\tURL: \"{tup.Item1}\"\n\tPATH: \"{tup.Item2}\"\n}}"));
			}

			return successCRLs;
		}

		public static bool InstallCertificateRevocationList(string crlFilepath)
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
