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

using ConversionUtilities;
using CertificateEnumerator;
using CertificateEnumerator.CertificateExtensionMethods;

namespace CertificateEnumeratorGUI
{
	public partial class MainForm : Form
	{
		Certificates certEnumerator;
		List<CertificateRow> certList = new List<CertificateRow>();

		public MainForm()
		{
			InitializeComponent();
			certEnumerator = new Certificates();
		}

		#region Form Event Handlers

		private void MainForm_Load(object sender, EventArgs e)
		{
			PopulateCells();
		}

		private void btnVerifyCerts_Click(object sender, EventArgs e)
		{
			certList.VerifyAllCerts();
			SetDataSource(certList);
		}

		private void btnSaveCellsAs_Click(object sender, EventArgs e)
		{
			SaveCellsAs();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			Search(tbSearch.Text);			
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

		#endregion

		private void Search(string value)
		{
			List<CertificateRow> newCertList = certEnumerator.GetCertificateRows().Where(cr => cr.Contains(value)).ToList();
			SetDataSource(newCertList);
		}

		private void PopulateCells()
		{
			certList = certEnumerator.GetCertificateRows();
			SetDataSource(certList);			
		}

		private void SetDataSource<T>(IEnumerable<T> source)
		{
			dataGridViewCertificates.DataSource = null;
			dataGridViewCertificates.DataSource = source;
		}

		private void SetDataSource(List<CertificateRow> certificateRow)
		{
			certList = certificateRow;
			SetDataSource<CertificateRow>(certList);

			HighlightExpiredCertificateCells();
		}

		private void HighlightExpiredCertificateCells()
		{
			DataGridViewCell effectiveDateCell;
			DataGridViewCell expirationDateCell;
			DateTime effectiveDate;
			DateTime expirationDate;
			foreach (DataGridViewRow row in dataGridViewCertificates.Rows)
			{
				effectiveDateCell = row.Cells["EffectiveDate"];
				expirationDateCell = row.Cells["ExpirationDate"];
				if (effectiveDateCell == null || expirationDateCell == null)
				{
					continue;
				}
				effectiveDate = (DateTime)effectiveDateCell.Value;
				expirationDate = (DateTime)expirationDateCell.Value;
				if (effectiveDate == null || expirationDate == null)
				{
					continue;
				}

				if (DateTime.Now.CompareTo(effectiveDate) < 0)
				{
					row.DefaultCellStyle.BackColor = Color.MistyRose;
					effectiveDateCell.ErrorText = "Effective date not reached.";
				}
				if (DateTime.Now.CompareTo(expirationDate) > 0)
				{
					row.DefaultCellStyle.BackColor = Color.MistyRose;
					expirationDateCell.ErrorText = "ExpirationDate date passed.";					
				}
			}
		}

		#region Save As Methods

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
				if (ExcelConverter.Convert(fileTemp, filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel9795))
				{
					return;
				}
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
