using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;

namespace ConversionUtilities
{
	public static class ExcelConverter
	{
		public static bool IsExcelInstalled()
		{
			Type officeType = Type.GetTypeFromProgID("Excel.Application");
			return (officeType != null);			
		}

		public static bool Convert(string from, string to, XlFileFormat format)
		{
			if (!IsExcelInstalled())
			{				
				return false;
			}

			Application app = new Application();
			app.DisplayAlerts = false;

			Workbook excelWorkbook = app.Workbooks.Open(from);
			excelWorkbook.SaveAs(to, format);
			excelWorkbook.Close();

			app.Quit();

			return File.Exists(to);
		}
	}
}
