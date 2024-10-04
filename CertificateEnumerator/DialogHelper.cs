using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace CertificateManagement
{
    public static class DialogHelper
    {
        private static string browseForFolderLocation = "";
        private static string browseForFileLocation = "";
        private static string saveFileLocation = "";        

        static DialogHelper()
        {
            string initialFolderLocation = "";
            try
            {
                initialFolderLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
            catch
            {
                initialFolderLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            browseForFileLocation = initialFolderLocation;
            browseForFileLocation = initialFolderLocation;
            saveFileLocation = initialFolderLocation;
        }

        public static string BrowseForFolderDialog(string initialDirectory)
        {
            browseForFolderLocation = initialDirectory;
            return BrowseForFolderDialog();
        }
        public static string BrowseForFolderDialog()
        {

            using (FolderBrowserDialog browseDialog = new FolderBrowserDialog())
            {
                browseDialog.SelectedPath = browseForFolderLocation;

                if (browseDialog.ShowDialog() == DialogResult.OK)
                {
                    browseForFolderLocation = Path.GetDirectoryName(browseDialog.SelectedPath);
                    return browseDialog.SelectedPath;
                }
            }

            return string.Empty;
        }

        public static string BrowseForFileDialog(string initialDirectory)
        {
            browseForFileLocation = initialDirectory;
            return BrowseForFileDialog();
        }
        public static string BrowseForFileDialog()
        {
            using (OpenFileDialog browseDialog = new OpenFileDialog())
            {
                browseDialog.InitialDirectory = browseForFileLocation;
                if (browseDialog.ShowDialog() == DialogResult.OK)
                {
                    browseForFileLocation = Path.GetDirectoryName(browseDialog.FileName);
                    return browseDialog.FileName;
                }
            }

            return string.Empty;
        }

        public static string SaveFileDialog(string initialFilename = "", string filters = "All Files|*.*")
        {
            using (SaveFileDialog browseDialog = new SaveFileDialog())
            {
                browseDialog.Filter = filters;
                browseDialog.InitialDirectory = saveFileLocation;
                if (!string.IsNullOrWhiteSpace(initialFilename))
                {
                    browseDialog.FileName = Path.Combine(saveFileLocation, initialFilename);
                }
                if (browseDialog.ShowDialog() == DialogResult.OK)
                {
                    saveFileLocation = Path.GetDirectoryName(browseDialog.FileName);
                    return browseDialog.FileName;
                }
            }
            return string.Empty;
        }
    }
}
