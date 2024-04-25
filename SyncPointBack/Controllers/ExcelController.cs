using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SyncPointBack.DTO;
using SyncPointBack.Model.Excel;
using SyncPointBack.Persistance;
using SyncPointBack.Services.Excel;
using System;
using System.Threading.Tasks;

namespace SyncPointBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly ILogger<ExcelController> _logger;
        private readonly IExcelService _excelService;
        private readonly SyncPointDb _syncPointDb;
        private readonly IMapper _mapper;

        public ExcelController(ILogger<ExcelController> logger, IExcelService excelService, SyncPointDb db, IMapper mapper)
        {
            _logger = logger;
            _excelService = excelService;
            _syncPointDb = db;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddRecord([FromBody] CreateExcelRecordDto recordEx)
        {
            ExcelRecord record = _mapper.Map<ExcelRecord>(recordEx);
            _logger.LogInformation("Excel Controller - AddRecord");
            if (recordEx == null)
            {
                return BadRequest("Invalid record data");
            }

            try
            {
                // Add the Excel record to the database asynchronously
                _syncPointDb.ExcelRecords.Add(record);
                await _syncPointDb.SaveChangesAsync();

                return Ok(recordEx);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the record: " + ex.Message);
            }
        }
    }
}