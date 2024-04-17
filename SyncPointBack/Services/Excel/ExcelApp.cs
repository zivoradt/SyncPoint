using Microsoft.Extensions.Logging;
using Microsoft.Office.Interop.Excel;
using SyncPointBack.Helper;
using SyncPointBack.Model.Excel;
using SyncPointBack.Services.Excel;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SyncPointBack.Services.ExcelInitiation
{
    public partial class ExcelApp : IExcelApp
    {
        private readonly ILogger<ExcelApp> _logger;

        private Func<IEnumerable<ExcelRecord>, Workbook, Worksheet> _addRecordsFunc = AddRecordsToExcel.AddRecords;

        private Application _excelApplication;

        public ExcelApp(ILogger<ExcelApp> logger)
        {
            _logger = logger;
        }

        public bool CreateExcel()
        {
            try
            {
                _excelApplication = InitiateExcel();

                if (_excelApplication == null)
                {
                    _logger.LogError("Excel Application failed to initiate.");
                    return false;
                }

                _logger.LogInformation("Excel Application initiated.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while creating Excel application: {ex.Message}");
                return false;
            }
        }

        public bool AddRecords(IEnumerable<ExcelRecord> records)
        {
            try
            {
                if (!CreateExcel())
                    return false;

                _logger.LogInformation("Creating Workbook and Sheet");
                Workbook workbook = CreateWorkbookAndSheet(_excelApplication, records);
                _logger.LogInformation("Workbook and Sheet created.");
                _logger.LogInformation("Populating tables in Excel");

                _logger.LogInformation("Records added to Excel.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to write to Excel: {ex.Message}");
                return false;
            }
            finally
            {
                ReleaseComObject(_excelApplication);
            }
        }

        private Workbook AddToWorksheet(IEnumerable<ExcelRecord> records, Workbook wb)
        {
            // Creating worksheet in excel
            Worksheet worksheet = (Worksheet)wb.Worksheets[1];

            string currentMonthName = DateTime.Now.ToString("MMM");

            // Set worksheet name of current month
            worksheet.Name = currentMonthName;

            worksheet = _addRecordsFunc(records, (Workbook)worksheet);

            return wb;
        }

        private Application InitiateExcel()
        {
            try
            {
                Application excelApp = new Microsoft.Office.Interop.Excel.Application();

                if (excelApp == null)
                {
                    throw new Exception("Excel is not installed!");
                }

                return excelApp;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while creating Excel application: {ex.Message}");
                return null;
            }
        }

        private Workbook CreateWorkbookAndSheet(Application xlApp, IEnumerable<ExcelRecord> record)
        {
            Workbook wb = xlApp.Workbooks.Add();

            wb = AddToWorksheet(record, wb);

            return wb;
        }

        private void ReleaseComObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while releasing COM object: {ex.Message}");
            }
        }
    }
}