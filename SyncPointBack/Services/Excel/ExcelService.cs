using SyncPointBack.Model.Excel;

namespace SyncPointBack.Services.Excel
{
    public class ExcelService : IExcelService
    {
        private readonly ILogger<ExcelService> _logger;

        public ExcelService(ILogger<ExcelService> logger)
        {
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