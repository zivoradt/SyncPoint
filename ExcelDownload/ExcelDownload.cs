using ClosedXML.Excel;
using ExcelDownload.Interface;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelDownload
{
    public class ExcelDownloader : IExcelDownload
    {
        private string GetCurrentMonthName()
        {
            return DateTime.Now.ToString("MMMM");
        }

        public MemoryStream GenerateExcelFile<T>(string fileName, IEnumerable<T> data, IEnumerable<string> headers)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("File name cannot be null or empty", nameof(fileName));
            }

            try
            {
                MemoryStream stream = new MemoryStream();

                XLWorkbook excel = new XLWorkbook();

                string worksheetName = GetCurrentMonthName();

                IXLWorksheet worksheet = excel.AddWorksheet(worksheetName);

                if (worksheet != null)
                {
                    int headerRowIndex = 1;
                    int dataRowIndex = headerRowIndex + 1;

                    int columnIndex = 1;
                    foreach (var header in headers)
                    {
                        worksheet.Cell(headerRowIndex, columnIndex).Value = header.ToString();
                        columnIndex++;
                    }
                    int emptyRowIndex = worksheet.RowsUsed().Count() + 1;

                    worksheet.Cell(emptyRowIndex, 1).InsertData(data);

                    excel.SaveAs(stream);
                    stream.Position = 0;
                    return stream;
                }
                else
                {
                    throw new InvalidOperationException("Failed to create worksheet.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while generating the Excel file.", ex);
            }
        }
    }
}