using CustomerLoan.API.Models;
using CustomerLoan.API.Models.DTO;

namespace CustomerLoan.API.Repository
{
    public interface ICurrencyExchangeRepository
    {
        Task<CurrencyExchangeRateDTO> GetCurrencyExchangeAsync(string type, DateTime date);
    }
}
