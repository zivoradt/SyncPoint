using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SyncPointBack.DTO;
using SyncPointBack.Helper.ErrorHandler.CustomException;
using SyncPointBack.Model.Excel;
using SyncPointBack.Persistance;
using SyncPointBack.Persistance.Interface;
using SyncPointBack.Utils;
using System.Linq.Expressions;
using ExcelDownload.Interface;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using System.Text.Json;

namespace SyncPointBack.Services.Excel
{
    public class ExcelService : ExcelServiceBase, IExcelService
    {
        private readonly ILogger<ExcelService> _logger;
        private UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IExcelDownload _excelDownload;

        public ExcelService(ILogger<ExcelService> logger, UnitOfWork unitOfWork, IMapper mapper, IExcelDownload excelDownload)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _excelDownload = excelDownload;
        }

        private string SerializeWithCustomOptions<T>(T obj)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            return JsonSerializer.Serialize(obj, options);
        }

        public async Task<ExcelRecord> AddRecord(ExcelRecord recordEx)
        {
            _logger.LogInformation("ExcelService: Trying Adding Record");

            try
            {
                recordEx.UserId = "zile";

                _unitOfWork.ExcelRepository.Insert(recordEx);

                await _unitOfWork.Save();

                _logger.LogInformation("ExcelService: Record added!");

                return recordEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MemoryStream> DownloadExcel()
        {
            try
            {
                IEnumerable<ExcelRecord> excelRecords = await GetAllRecordsFromThisMonth();

                List<ExcelImportDto> excelImportDtos = new List<ExcelImportDto>();

                foreach (ExcelRecord recordEx in excelRecords)
                {
                    var record = _mapper.Map<ExcelImportDto>(recordEx);

                    excelImportDtos.Add(record);
                }

                MemoryStream stream = _excelDownload.GenerateExcelFile("TestExcel", excelImportDtos, IncludeEntities);

                return stream;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);

                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

                throw;
            }
        }

        public async Task<IEnumerable<ExcelRecord>> GetAllRecordsFromThisMonth()
        {
            DateTime currentDate = DateTime.Today;
            DateTime firstDayOfMont = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMont.AddMonths(1).AddDays(-1);

            try
            {
                _logger.LogInformation("Searching DB for records");

                IEnumerable<ExcelRecord> excelRecords = _unitOfWork.ExcelRepository.Get(filter: x => x.EndDate >= firstDayOfMont && x.EndDate <= lastDayOfMonth, includeProperties: Entities).OrderBy(x => x.TicketId).ToList();

                if (!excelRecords.Any())
                {
                    return Enumerable.Empty<ExcelRecord>();
                }

                return excelRecords;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ExcelRecordToClientDto>> GetTodayRecords()
        {
            try
            {
                var records = _unitOfWork.ExcelRepository.Get(x => EF.Functions.DateDiffDay(x.StartDate, DateTime.Today) == 0).OrderBy(x => x.TicketId).ToList();

                if (!records.Any())
                {
                    return Enumerable.Empty<ExcelRecordToClientDto>();
                }

                var recordsToClientDtoList = new List<ExcelRecordToClientDto>();

                foreach (var record in records)
                {
                    var recordToClient = _mapper.Map<ExcelRecordToClientDto>(record);
                    recordsToClientDtoList.Add(recordToClient);
                }

                return recordsToClientDtoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ExcelRecordToClientDto>> GetByClientId(string clientId)
        {
            _logger.LogInformation("Trying to get Excel Recod by client ID");

            string[] includeHeaders = IncludeEntities.ToArray();

            IEnumerable<ExcelRecord> excelRecords = _unitOfWork.ExcelRepository.Get(filter: x => x.UserId == clientId, includeProperties: includeHeaders).OrderBy(x => x.TicketId).ToList();

            if (!excelRecords.Any())
            {
                return Enumerable.Empty<ExcelRecordToClientDto>();
            }

            var recordsToClientDtoList = new List<ExcelRecordToClientDto>();

            foreach (var record in excelRecords)
            {
                var recordToClient = _mapper.Map<ExcelRecordToClientDto>(record);

                recordsToClientDtoList.Add(recordToClient);
            }

            return recordsToClientDtoList;
        }

        public async Task<bool> isExist(string UserId)
        {
            return _unitOfWork.ExcelRepository.Get(x => x.UserId == UserId) == null;
        }

        public async Task<bool> isExist(int TicketId)
        {
            return _unitOfWork.ExcelRepository.Get(x => x.Id == TicketId) == null;
        }

        public async Task DeleteExcelRecord(int RecordID)
        {
            try
            {
                _unitOfWork.ExcelRepository.Delete(RecordID);

                await _unitOfWork.Save();

                _logger.LogInformation("The record is succesfully deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting ExcelRecord with ID {RecordID}: {ex.Message}");

                throw;
            }
        }
    }
}