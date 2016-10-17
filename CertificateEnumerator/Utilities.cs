using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace CertificateEnumerator
{
    public static class Utilities
    {
        public static string EnsureFilenameNotExists(string filename)
        {
            int counter = 1;
            string result = filename;
            string extension = Path.GetExtension(result);
            while(File.Exists(result))
            {
                result = Path.ChangeExtension(result, string.Concat(".", counter.ToString().PadLeft(3,'0'), extension));
                counter++;
            }

            return result;
        }

        public static List<X509Certificate2> CertsFromFolder(string path)
        {
            if(string.IsNullOrWhiteSpace(path)) { throw new ArgumentException("Argument path must not be null, empty or whitespace", "path"); }
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

        private static int webRequestTimeout = 2000;
        public static List<string> DownloadFiles(List<string> remoteFileURIs)
        {
            List<string> successCRLs = new List<string>();
            List<string> erroredCRLs = new List<string>();

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
                        if (string.IsNullOrWhiteSpace(filename)) { continue; }
                        localFile = Path.Combine(Path.GetTempPath(), filename);

                        // TODO: Make asynch                        
                        client.DownloadFile(remoteFile, localFile);

                        if (File.Exists(localFile))
                        {
                            successCRLs.Add(localFile);
                        }
                        else
                        {
                            erroredCRLs.Add(localFile);
                        }
                    }
                    catch (WebException)
                    {
                        erroredCRLs.Add(localFile);
                    }
                }
            }

            return successCRLs;
        }

        public static bool InstallCertificate(string certificateFileName)
        {
            if (!File.Exists(certificateFileName))
            {
                return false;
            }

            try
            {
                bool result = false;
                X509Store store = new X509Store(StoreName.Disallowed, StoreLocation.LocalMachine);
                X509Certificate2 certificate = new X509Certificate2(certificateFileName);
                store.Open(OpenFlags.ReadWrite);
                bool certificateExists = store.Certificates.Contains(certificate);

                if (certificateExists)
                {
                    result = true;
                }
                else
                {
                    //store.Add(certificate);                    
                    result = true;
                }
                store.Close();
                store = null;
                return result;
            }
            catch
            {
                return false;
            }
        }
    }
}
