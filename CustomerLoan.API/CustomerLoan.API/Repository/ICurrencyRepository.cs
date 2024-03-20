using CustomerLoan.API.Models;

namespace CustomerLoan.API.Repository
{
    public interface ICurrencyRepository
    {
        Task<List<Currency>> GetCurrenciesAsync();
    }
}
