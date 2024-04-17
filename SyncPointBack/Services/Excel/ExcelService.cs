using SyncPointBack.Model.Excel;
using SyncPointBack.Services.ExcelInitiation;

namespace SyncPointBack.Services.Excel
{
    public class ExcelService : IExcelService
    {
        private ExcelApp _excelStart;

        private readonly ILogger<ExcelService> _logger;

        public ExcelService(ExcelApp excelStart, ILogger<ExcelService> logger)
        {
            _excelStart = excelStart;
            _logger = logger;
        }

        public ExcelRecord AddRecord(ExcelRecord recordEx)
        {
            _logger.LogInformation("ExcelService: Adding Record");

            _logger.LogInformation("ExcelService: Record added!");

            return recordEx;
        }
    }
}