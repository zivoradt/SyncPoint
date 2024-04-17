using SyncPointBack.Model.Excel;

namespace SyncPointBack.Services.Excel
{
    public interface IExcelService
    {
        public ExcelRecord AddRecord(ExcelRecord recordEx);
    }
}