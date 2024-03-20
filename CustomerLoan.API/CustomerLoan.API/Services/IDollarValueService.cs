using CustomerLoan.API.Models;
using CustomerLoan.API.Models.DTO;

namespace CustomerLoan.API.Services
{
    public interface IDollarValueService
    {
        Task<DollarValueDTO> GetDollarValueAsync(DateTime date);
    }
}
