using CustomerLoan.API.Models;
using CustomerLoan.API.Repository;
using CustomerLoan.API.Models.DTO;

namespace CustomerLoan.API.Services
{
    public class CurrencyExchangeServices : ICurrencyExchangeService
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

        public CurrencyExchangeServices(ICurrencyExchangeRepository currencyExchangeRepository)
        {
            _currencyExchangeRepository = currencyExchangeRepository ?? throw new ArgumentNullException(nameof(currencyExchangeRepository));
        }

        public async Task<CurrencyExchangeRateDTO> GetCurrencyExchangeAsync(string type, DateTime date)
        {
            return await _currencyExchangeRepository.GetCurrencyExchangeAsync(type, date);
        }

    }
}
