using CustomerLoan.API.Models;
using CustomerLoan.API.Models.DTO;

namespace CustomerLoan.API.Repository
{
    public interface IDollarValueRepository
    {
        Task<DollarValueDTO> GetDollarValueAsync(DateTime date);
    }
}
