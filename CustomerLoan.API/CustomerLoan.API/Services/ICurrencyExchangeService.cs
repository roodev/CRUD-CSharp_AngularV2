using CustomerLoan.API.Models;
using CustomerLoan.API.Models.DTO;

namespace CustomerLoan.API.Services
{
    public interface ICurrencyExchangeService
    {
        Task<CurrencyExchangeRateDTO> GetCurrencyExchangeAsync(string type, DateTime date);
    }
}
