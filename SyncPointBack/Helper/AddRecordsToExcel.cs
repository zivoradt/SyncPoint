using Microsoft.Office.Interop.Excel;
using SyncPointBack.Model.Excel;
using SyncPointBack.Services.ExcelInitiation;

namespace SyncPointBack.Helper
{
    public static class AddRecordsToExcel
    {
        public static Worksheet AddRecords(IEnumerable<ExcelRecord> record, Workbook worksheet)
        {
            int getNextEmptyRow = IsRowEmpty((Worksheet)worksheet);

            return (Worksheet)worksheet;
        }

        private static int IsRowEmpty(Worksheet woorksheet)
        {
            int currentRow = 1;

            int maximumRows = 1000;
            while (currentRow < maximumRows)
            {
                Microsoft.Office.Interop.Excel.Range row = woorksheet.Rows[currentRow];

                bool isRowEmpty = true;

                foreach (Microsoft.Office.Interop.Excel.Range cells in row.Cells)
                {
                    if (!string.IsNullOrEmpty(cells.Text))
                    {
                        isRowEmpty = false;
                        break;
                    }
                }

                if (isRowEmpty)
                {
                    return currentRow;
                }
                currentRow++;
            }

            return currentRow;
        }

        private static Worksheet PopulateCells(IEnumerable<ExcelRecord> records, Worksheet worksheet, int rowToStartPopulateData)
        {
            return (Worksheet)worksheet;
        }
    }
}