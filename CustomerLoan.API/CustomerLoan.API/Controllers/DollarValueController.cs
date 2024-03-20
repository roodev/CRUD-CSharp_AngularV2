using CustomerLoan.API.Models;
using CustomerLoan.API.Models.DTO;
using CustomerLoan.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerLoan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DollarValueController : ControllerBase
    {
        private readonly IDollarValueService _dollarValueService;

        public DollarValueController(IDollarValueService dollarValueService)
        {
            _dollarValueService = dollarValueService ?? throw new ArgumentNullException(nameof(dollarValueService));
        }

        [HttpGet("{date}")]
        public async Task<ActionResult<DollarValueDTO>> GetDollarValue(DateTime date)
        {
            try
            {
                var dollarValue = await _dollarValueService.GetDollarValueAsync(date);
                return Ok(dollarValue);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }      
    }
}
