using SyncPointBack.DTO;
using SyncPointBack.Model.Excel;

namespace SyncPointBack.Services.Excel
{
    public abstract class ExcelServiceBase
    {
        protected readonly int DefaultMonth = DateTime.Now.Month;

        protected readonly string[] Entities = { "PIM", "StaticPageCreation", "StaticPageModification", "PDRegistration", "PDModification", "PIM", "GNB" };

        public readonly IEnumerable<string> IncludeEntities = new List<string>
    {
        "Date", "Start Time", "Ticket ID", "StaticPageCreation", "StaticPageModification", "PDRegistration", "PDModification", "PIM", "GNB", "Other", "No. of changes", "Description", "Finish Time", "Production Time"
    };
    }

    public interface IExcelService
    {
        public Task<ExcelRecord> AddRecord(ExcelRecord recordEx);

        public Task<IEnumerable<ExcelRecordToClientDto>> GetByClientId(string clientId);

        public Task<IEnumerable<ExcelRecordToClientDto>> GetTodayRecords();

        Task DeleteExcelRecord(int id);

        Task<MemoryStream> DownloadExcel();

        Task<bool> isExist(int TicketID);

        Task<bool> isExist(string UserID);

        Task<List<ExcelRecordToClientDto>> GetAllRecordsFromThisMonth();
    }
}