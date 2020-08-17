using System.Windows.Forms;

namespace CertificateEnumeratorGUI
{
	public static class DialogHelper
	{
		public static string BrowseForFolderDialog(string initialDirectory = "")
		{
			using (FolderBrowserDialog browseDialog = new FolderBrowserDialog())
			{
				browseDialog.SelectedPath = initialDirectory;
				//browseDialog.RootFolder = Environment.SpecialFolder.ProgramFilesX86;
				if (browseDialog.ShowDialog() == DialogResult.OK)
				{
					return browseDialog.SelectedPath;
				}
			}

			return string.Empty;
		}

		public static string BrowseForFileDialog(string initialDirectory = "")
		{
			using (OpenFileDialog browseDialog = new OpenFileDialog())
			{
				browseDialog.InitialDirectory = initialDirectory;
				//browseDialog.RootFolder = Environment.SpecialFolder.ProgramFilesX86;
				if (browseDialog.ShowDialog() == DialogResult.OK)
				{
					return browseDialog.FileName;
				}
			}

			return string.Empty;
		}

		public static string SaveFileDialog(string initialDirectory = "")
		{
			using (SaveFileDialog browseDialog = new SaveFileDialog())
			{
				browseDialog.InitialDirectory = initialDirectory;
				//browseDialog.RootFolder = Environment.SpecialFolder.ProgramFilesX86;
				if (browseDialog.ShowDialog() == DialogResult.OK)
				{
					return browseDialog.FileName;
				}
			}

			return string.Empty;
		}
	}
}
