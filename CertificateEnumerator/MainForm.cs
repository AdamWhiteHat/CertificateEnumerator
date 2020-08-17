using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace CertificateEnumeratorGUI
{
	public partial class MainForm : Form
	{
		private CertificateRowCollection certificateRowCollection;

		public MainForm()
		{
			InitializeComponent();
		}

		#region Form Event Handlers

		private void MainForm_Load(object sender, EventArgs e)
		{
			PopulateCells();
		}

		private void btnSaveCellsAs_Click(object sender, EventArgs e)
		{
			SaveCellsAs();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			Search(tbSearch.Text);
		}

		private void btnDownloadCRLs_Click(object sender, EventArgs e)
		{
			DownloadCRLs();
		}

		private void btnInstallCRLs_Click(object sender, EventArgs e)
		{
			InstallCRLs();
		}

		private void tbSearch_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				Search(tbSearch.Text);
			}
		}

		private void dataGridViewCertificates_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Modifiers == Keys.Control)
			{
				if (e.KeyData == Keys.S)
				{
					SaveCellsAs();
				}
			}
			else if (e.KeyData == Keys.F5)
			{
				PopulateCells();
			}
		}

		private void btnGetPublicKey_Click(object sender, EventArgs e)
		{
			List<string> publicKeys = certificateRowCollection.GetAllCertificatesPublicKeyValues();

			string filename = Utilities.EnsureFilenameNotExists(Utilities.PublicKeysStoreOutputFilename);
			File.WriteAllLines(filename, publicKeys);
		}

		private void btnSearchFolder_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dlg = new FolderBrowserDialog())
			{
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					List<X509Certificate2> certs = Utilities.SearchForCertsInFolder(dlg.SelectedPath);
					CertificateRowCollection certRowCollection = CertificateRowCollection.FromList(certs);
					List<string> publicKeys = certRowCollection.GetAllCertificatesPublicKeyValues();

					string filename = Utilities.EnsureFilenameNotExists(Utilities.PublicKeysFolderOutputFilename);
					File.WriteAllLines(filename, publicKeys);
				}
			}
		}

		private void dataGridViewCertificates_Sorted(object sender, EventArgs e)
		{
			HighlightExpiredCertificateCells();
		}

		#endregion

		#region Feature Methods

		private void DownloadCRLs()
		{
			List<string> userSpecifiedCRLs = Utilities.GetCRLsToDownloadURLs();
			if (userSpecifiedCRLs.Any())
			{
				List<Tuple<string, string>> successfullyDownloadedCRLs2 = Utilities.DownloadFiles(userSpecifiedCRLs);
			}

			if (dataGridViewCertificates.SelectedRows.Count > 0)
			{
				var selectedRows = dataGridViewCertificates.SelectedRows.Cast<DataGridViewRow>().ToList();
				var selectedCertRows = selectedRows.Select(row => (CertificateRow)row.DataBoundItem).ToList();
				var selectedCrlUrls = selectedCertRows.SelectMany(certRow => certRow.GetCertificateRevocationListURLs()).Distinct().ToList();
				List<Tuple<string, string>> successfullyDownloadedSelectedCRLs = Utilities.DownloadFiles(selectedCrlUrls);
			}
			else
			{
				List<string> certStoreCRLs = certificateRowCollection.GetAllCertificatesRevocationListURLs();
				if (certStoreCRLs.Any())
				{
					// Tuple<string, string> of the form: <RemoteFile, LocalFile>
					List<Tuple<string, string>> successfullyDownloadedCRLs = Utilities.DownloadFiles(certStoreCRLs);
				}
			}
		}

		private void InstallCRLs()
		{
			List<string> toInstallCRLs = Directory.EnumerateFiles(Utilities.DownloadPath, "*.crl").ToList();
			List<string> installedCRLs = Utilities.InstallCertificateRevocationLists(toInstallCRLs);
		}

		private void Search(string value)
		{
			List<CertificateRow> newCertList = certificateRowCollection.Where(cr => cr.ContainsString(value)).ToList();
			SetDataSource(newCertList);
		}

		private void PopulateCells()
		{
			certificateRowCollection = new CertificateRowCollection();
			SetDataSource(certificateRowCollection);
		}

		private void SetDataSource(List<CertificateRow> certificateRow)
		{
			SortableBindingList<CertificateRow> boundList = new SortableBindingList<CertificateRow>(certificateRow);
			boundList.AllowEdit = false;
			boundList.AllowNew = false;
			boundList.AllowRemove = false;

			dataGridViewCertificates.DataSource = null;
			dataGridViewCertificates.DataSource = boundList;
			dataGridViewCertificates.Columns["HasErrors"].Visible = false;
			dataGridViewCertificates.Columns["ErrorText"].Visible = false;
			dataGridViewCertificates.Columns["ErrorProperty"].Visible = false;

			dataGridViewCertificates.Columns["IsVerified"].SortMode = DataGridViewColumnSortMode.Automatic;
			dataGridViewCertificates.Columns["HasPrivateKey"].SortMode = DataGridViewColumnSortMode.Automatic;
			dataGridViewCertificates.Columns["HasCrlDistributionPoint"].SortMode = DataGridViewColumnSortMode.Automatic;

			HighlightExpiredCertificateCells();
		}

		private void HighlightExpiredCertificateCells()
		{
			certificateRowCollection.VerifyAllCerts();

			foreach (DataGridViewRow row in dataGridViewCertificates.Rows)
			{
				CertificateRow certRow = (CertificateRow)row.DataBoundItem;
				if (certRow.HasErrors)
				{
					row.DefaultCellStyle.BackColor = Color.MistyRose;
					DataGridViewCell cell = row.Cells[certRow.ErrorProperty];
					if (cell != null)
					{
						cell.ErrorText = certRow.ErrorText;
					}
				}
			}
		}

		#endregion

		#region Save Methods

		private void SaveCellsAs()
		{
			if (dataGridViewCertificates.DataSource == null || dataGridViewCertificates.RowCount < 1)
			{
				MessageBox.Show("Nothing to save! Try hitting F5 to refresh.");
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
					case ".xls":
					case ".xlsx":
						SaveCells_AsXLSX(filename);
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

		private void SaveCells_AsXLSX(string filename)
		{
			string fileTemp = Path.GetTempFileName();
			SaveCells_AsCSV(fileTemp);
			if (File.Exists(fileTemp))
			{
				//if (ExcelConverter.Convert(fileTemp, filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel9795))
				//{
				return;
				//}
			}
			MessageBox.Show("Unable to convert file to EXCEL file. Perhaps excel is not installed?");
		}

		IDataObject objectSave = null;
		private void CopyCellsToClipboard()
		{
			// Choose whether to write header. You will want to do this for a CSV file.
			dataGridViewCertificates.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
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

		#endregion

	}
}
