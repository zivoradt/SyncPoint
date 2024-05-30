using SyncPointBack.DTO;
using SyncPointBack.Model.Excel;

namespace SyncPointBack.Services.Excel
{
    public abstract class ExcelServiceBase
    {
        protected readonly int DefaultMonth = DateTime.Now.Month;
        protected static readonly string[] IncludeEntities = { "PIM", "StaticPageCreation", "StaticPageModification", "PDRegistration", "PDModification", "PIM", "GNB" };
    }

    public interface IExcelService
    {
        public Task<ExcelRecord> AddRecord(ExcelRecord recordEx);

        public Task<IEnumerable<ExcelRecordToClientDto>> GetByClientId(string clientId);

        public Task<IEnumerable<ExcelRecordToClientDto>> GetTodayRecords();

        Task DeleteExcelRecord(int id);

        Task<bool> isExist(int TicketID);

        Task<bool> isExist(string UserID);

        Task<IEnumerable<ExcelRecord>> GetAllRecordsFromThisMonth();
    }
}