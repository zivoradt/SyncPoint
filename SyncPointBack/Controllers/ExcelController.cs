using AutoMapper;
using ExcelDownload.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncPointBack.DTO;
using SyncPointBack.Helper.JWTMiddleware;
using SyncPointBack.Model.Excel;
using SyncPointBack.Services.Excel;
using System.Security.Claims;

namespace SyncPointBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly ILogger<ExcelController> _logger;
        private readonly IExcelService _excelService;
        private readonly IMapper _mapper;

        public ExcelController(ILogger<ExcelController> logger, IExcelService excelService, IMapper mapper)
        {
            _logger = logger;
            _excelService = excelService;
            _mapper = mapper;
        }

        private IActionResult HandleError(Exception ex, string customMessage)
        {
            _logger.LogError(ex, customMessage);
            return StatusCode(StatusCodes.Status500InternalServerError, customMessage);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> AddRecord([FromBody] CreateExcelRecordDto recordEx)
        {
            var record = _mapper.Map<ExcelRecord>(recordEx);
            _logger.LogInformation("Excel Controller - Trying to AddRecord");

            try
            {
                await _excelService.AddRecord(record);
                return Ok(recordEx);
            }
            catch (Exception ex)
            {
                return HandleError(ex, "The record could not be saved to the database. Please try again later.");
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet("CurrentMonth")]
        public async Task<IActionResult> GetRecordsFromCurrentMonth()
        {
            var userId = User.GetUserId();

            if (userId == null)
            {
                return BadRequest();
            }

            try
            {
                var currentMonthRecords = await _excelService.GetAllRecordsFromThisMonth();
                return Ok(currentMonthRecords);
            }
            catch (Exception ex)
            {
                return HandleError(ex, ex.Message);
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
                return HandleError(ex, "An error occurred while processing your request. Please try again later.");
            }
        }

        [HttpGet("DownloadExcelFile")]
        public async Task<IActionResult> DownloadExcelFile()
        {
            try
            {
                var stream = await _excelService.DownloadExcel();
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TestExcel.xlsx");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "An error occurred while processing your request. Please try again later.");
            }
        }

        [HttpGet("{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExcelRecordToClientDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRecordFromClientId(string clientId)
        {
            _logger.LogInformation("Getting record from client ID.");

            try
            {
                var excelRecords = await _excelService.GetByClientId(clientId);

                if (!excelRecords.Any())
                {
                    return NotFound();
                }

                return Ok(excelRecords);
            }
            catch (Exception ex)
            {
                return HandleError(ex, "An error occurred while processing your request. Please try again later.");
            }
        }

        [HttpDelete("{RecordId}")]
        public async Task<IActionResult> DeleteRecord(int RecordId)
        {
            _logger.LogInformation("Trying to delete Record");

            try
            {
                if (!await _excelService.isExist(RecordId))
                {
                    return NotFound($"Ticket with {RecordId} doesn't exist.");
                }

                await _excelService.DeleteExcelRecord(RecordId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleError(ex, "An error occurred while processing your request. Please try again later.");
            }
        }
    }
}