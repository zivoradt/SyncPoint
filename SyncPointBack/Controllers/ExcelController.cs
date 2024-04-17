using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncPointBack.Model.Excel;
using SyncPointBack.Services.Excel;

namespace SyncPointBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly ILogger<ExcelController> _logger;

        private readonly IExcelService _excelService;

        public ExcelController(ILogger<ExcelController> logger, IExcelService excelService)
        {
            _logger = logger;
            _excelService = excelService;
        }

        [HttpPost]
        public IActionResult AddRecord([FromBody()] Record recordEx)
        {
            _logger.LogInformation("Pin Controller");
            if (recordEx == null)
            {
                return BadRequest("Invalid record data");
            }

            try
            {
                Record record = _excelService.AddRecord(recordEx);

                return Ok(record);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the record: " + ex.Message);
            }
        }
    }
}