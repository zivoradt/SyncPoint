using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SyncPointBack.DTO;
using SyncPointBack.Helper.ErrorHandler.CustomException;
using SyncPointBack.Model.Excel;
using SyncPointBack.Persistance;
using SyncPointBack.Services.Excel;

namespace SyncPointBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly ILogger<ExcelController> _logger;
        private readonly IExcelService _excelService;
        private readonly IMapper _mapper;

        public ExcelController(ILogger<ExcelController> logger, IExcelService excelService, SyncPointDb db, IMapper mapper)
        {
            _logger = logger;
            _excelService = excelService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddRecord([FromBody] CreateExcelRecordDto recordEx)
        {
            ExcelRecord record = _mapper.Map<ExcelRecord>(recordEx);

            _logger.LogInformation("Excel Controller - Trying to AddRecord");

            try
            {
                await _excelService.AddRecord(record);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");

                return StatusCode(500, "The record could not be saved to the database. Please try again later.");
            }

            return Ok(recordEx);
        }

        [HttpGet("MontlyRecords")]
        public async Task<IActionResult> GetRecordsFromCurrentMonth()
        {
            try
            {
                var currentMonthRecords = await _excelService.GetAllRecordsFromThisMonth();

                return Ok(currentMonthRecords);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("TodaysRecords")]
        public async Task<IActionResult> GetTodayRecords()
        {
            _logger.LogInformation("Getting all records from today.");

            try
            {
                var records = await _excelService.GetTodayRecords();

                return Ok(records);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request. Please try again later.");
            }
        }

        [HttpGet("GetRecordByID/{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExcelRecordToClientDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRecordFromClientId(string clientId)
        {
            _logger.LogInformation("Getting record from client ID.");

            IEnumerable<ExcelRecordToClientDto> excelRecod = await _excelService.GetByClientId(clientId);

            if (!excelRecod.Any())
            {
                return NotFound();
            }

            return Ok(excelRecod);
        }

        [HttpDelete("Delete/{RecordId}")]
        public async Task<IActionResult> DeleteRecord(int RecordId)
        {
            _logger.LogInformation("Trying to delete Record");

            if (!await _excelService.isExist(RecordId))
            {
                return NotFound($"Ticket with {RecordId} don't exist.");
            }

            await _excelService.DeleteExcelRecord(RecordId);

            return Ok(StatusCodes.Status204NoContent);
        }
    }
}