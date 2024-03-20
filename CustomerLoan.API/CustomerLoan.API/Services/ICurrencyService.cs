using CustomerLoan.API.Models;
using CustomerLoan.API.Repository;

namespace CustomerLoan.API.Services
{
    public interface ICurrencyService
    {
        Task<List<Currency>> GetCurrenciesAsync();
    }
}
