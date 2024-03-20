using CustomerLoan.API.Models;
using CustomerLoan.API.Models.DTO;
using CustomerLoan.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerLoan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly ICurrencyExchangeService _currencyExchangeService;

        public CurrencyExchangeController(ICurrencyExchangeService currencyExchangeService)
        {
            _currencyExchangeService = currencyExchangeService ?? throw new ArgumentNullException(nameof(currencyExchangeService));
        }

        [HttpGet("{type}/{date}")]
        public async Task<ActionResult<CurrencyExchangeRateDTO>> GetCurrencyExchangeAsync(string type, DateTime date)
        {
            try
            {
                var currencyExchange = await _currencyExchangeService.GetCurrencyExchangeAsync(type, date);
                return Ok(currencyExchange);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
