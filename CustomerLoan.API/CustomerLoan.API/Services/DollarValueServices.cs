using CustomerLoan.API.Models;
using CustomerLoan.API.Models.DTO;
using CustomerLoan.API.Repository;

namespace CustomerLoan.API.Services
{
    public class DollarValueServices : IDollarValueService
    {
        private readonly IDollarValueRepository _dollarValueRepository;

        public DollarValueServices(IDollarValueRepository dollarValueRepository)
        {
            _dollarValueRepository = dollarValueRepository ?? throw new ArgumentNullException(nameof(dollarValueRepository));
        }

        public async Task<DollarValueDTO> GetDollarValueAsync(DateTime date)
        {
            return await _dollarValueRepository.GetDollarValueAsync(date);
        }
    }
}
