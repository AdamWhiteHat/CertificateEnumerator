using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using CertificateEnumerator;


namespace CertificateEnumeratorGUI
{
	public partial class MainForm : Form
	{
		Certificates certEnumerator;
		List<CertificateEnumerator.CertificateRow> certList = new List<CertificateEnumerator.CertificateRow>();

		public MainForm()
		{
			InitializeComponent();
			certEnumerator = new Certificates();
		}

		private void DisplayOutput(string format, params object[] args)
		{
			tbOutput.Text += string.Concat(string.Format(format, args), Environment.NewLine);
		}

		private void DisplayClear()
		{
			tbOutput.Text = string.Empty;
		}

		private void btnEnumerate_Click(object sender, EventArgs e)
		{
			DisplayClear();

			certList = certEnumerator.GetCertificateRows();
			dataGridViewCertificates.DataSource = certList;

			tbOutput.Text = certEnumerator.GetCertificateString();
		}

		private void btnVerifyCerts_Click(object sender, EventArgs e)
		{
			foreach (CertificateEnumerator.CertificateRow certRow in certList)
			{
				certRow.Verify();
			}
		}

		private void ShowNothingToSaveMessage()
		{
			MessageBox.Show("Nothing to save! Try clicking the 'Enumerate' button first, then 'Save as...'");
		}

		private void btnSaveTextAs_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(tbOutput.Text))
			{
				ShowNothingToSaveMessage();
				return;
			}
			if (saveFileDialogText.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string filename = saveFileDialogText.FileName;
				File.WriteAllLines(filename, tbOutput.Lines);
			}
		}

		private void btnSaveCellsAs_Click(object sender, EventArgs e)
		{
			if (dataGridViewCertificates.DataSource == null || dataGridViewCertificates.RowCount < 1)
			{
				ShowNothingToSaveMessage();
				return;
			}
			if (saveFileDialogSelectedCells.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string filename = saveFileDialogSelectedCells.FileName;
				string extension = Path.GetExtension(filename);

				CopyCellsToClipboard();

				switch (extension)
				{
					case ".txt":
						SaveCells_AsTEXT(filename);
						break;
					case ".htm":
					case ".html":
						SaveCells_AsHTML(filename);
						break;
					case ".csv":
						SaveCells_AsCSV(filename);
						break;
					default:
						SaveCells_AsTEXT(filename);
						break;
				}
			}						
		}

		private void SaveCells_AsTEXT(string filename)
		{
			string clipBoard = PasteClipboard(TextDataFormat.Text);
			File.WriteAllText(filename, clipBoard);			
		}

		private void SaveCells_AsCSV(string filename)
		{
			string clipBoard = PasteClipboard(TextDataFormat.CommaSeparatedValue);
			File.WriteAllText(filename, clipBoard);	
		}

		private void SaveCells_AsHTML(string filename)
		{
			string clipBoard = PasteClipboard(TextDataFormat.Html);

			int start = clipBoard.IndexOf("<HTML>");
			clipBoard = clipBoard.Substring(start);

			File.WriteAllText(filename, clipBoard);	
		}

		IDataObject objectSave = null;
		private void CopyCellsToClipboard()
		{
			// Choose whether to write header. You will want to do this for a CSV file.
			dataGridViewCertificates.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
			// Save the current state of the clipboard so we can restore it after we are done
			IDataObject objectSave = Clipboard.GetDataObject();
			// Select the cells we want to serialize.
			dataGridViewCertificates.SelectAll();
			// Copy (set clipboard)
			Clipboard.SetDataObject(dataGridViewCertificates.GetClipboardContent());
		}

		private string PasteClipboard(TextDataFormat format)
		{
			string result = Clipboard.GetText(format);
			// Restore the current state of the clipboard so the effect is seamless
			if (objectSave != null)
			{
				Clipboard.SetDataObject(objectSave);
			}
			return result;
		}
	}
}
