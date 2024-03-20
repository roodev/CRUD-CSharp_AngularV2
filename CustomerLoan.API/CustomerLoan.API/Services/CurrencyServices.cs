using CustomerLoan.API.Models;
using CustomerLoan.API.Repository;

namespace CustomerLoan.API.Services
{
    public class CurrencyServices : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyServices(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<List<Currency>> GetCurrenciesAsync()
        {
            return await _currencyRepository.GetCurrenciesAsync();
        }
    }
}
