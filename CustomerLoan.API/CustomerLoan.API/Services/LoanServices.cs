using Microsoft.EntityFrameworkCore.Diagnostics;
using CustomerLoan.API.Models;
using CustomerLoan.API.Repository;
using CustomerLoan.API.Models.DTO;


namespace CustomerLoan.API.Services
{
    public class LoanServices : ILoanService<Loan>
    {
        private readonly ILoanRepository loanRepository;
        private readonly IDollarValueRepository dollarValueRepository;
        private readonly ICurrencyExchangeRepository currencyExchangeRepository;
        public LoanServices(ILoanRepository _loanRepository, IDollarValueRepository _dollarValueRepository, ICurrencyExchangeRepository _currencyExchangeRepository)
        {
            loanRepository = _loanRepository ?? throw new ArgumentNullException(nameof(_loanRepository));
            dollarValueRepository = _dollarValueRepository ?? throw new ArgumentNullException(nameof(_dollarValueRepository));
            currencyExchangeRepository = _currencyExchangeRepository ?? throw new ArgumentNullException(nameof(_currencyExchangeRepository));
            
        }

        public bool Add(Loan register)
        {
            if (register == null) throw new ArgumentNullException(nameof(register), "Registro nulo");
            loanRepository.AddLoan(register);
            return loanRepository.SaveChanges();
        }

        public bool Delete(int id)
        {
            if (id == 0 || id == null) throw new ArgumentNullException("Id não válido");
            loanRepository.DeleteLoan(id);
            return loanRepository.SaveChanges();
        }

        public IEnumerable<Loan> List()
        {
            return loanRepository.GetAllLoans();
        }

        public bool Update(Loan register)
        {
            if (register == null) throw new ArgumentNullException("Registro nulo");
            if (register.Id == 0 || register.Id == null) throw new ArgumentNullException("Id não válido");
            loanRepository.UpdateLoan(register);
            return loanRepository.SaveChanges();
        }

        public Loan GetLoanById(int id)
        {
            if (id == 0) throw new ArgumentException("Id não válido", nameof(id));
            return loanRepository.GetLoanById(id);
        }

        public int CalculateTotalMonths(DateTime startDate, DateTime endDate)
        {
            TimeSpan duration = endDate - startDate;
            int totalMonths = (int)Math.Ceiling(duration.TotalDays / 30.0);
            return totalMonths;
        }

        public async Task<decimal> GetDollarValueAsync(DateTime date)
        {
            DollarValueDTO dollarValue = await dollarValueRepository.GetDollarValueAsync(date);

            decimal dollartoday = dollarValue.CotacaoVenda;

            return dollartoday;
        }

        public async Task<decimal> ConvertCurrencyAsync(string selectedCurrencyType, decimal loanAmount, DateTime loanDate)
        {
            decimal dollarValue = await GetDollarValueAsync(loanDate);

            var currencyExchange = await currencyExchangeRepository.GetCurrencyExchangeAsync(selectedCurrencyType, loanDate);

            decimal convertedAmount;

            if (selectedCurrencyType == "A")
            {
                decimal purchaseQuote = currencyExchange.CotacaoCompra / currencyExchange.ParidadeVenda;
                convertedAmount = loanAmount * purchaseQuote;
            }
            else if (selectedCurrencyType == "B")
            {
                decimal saleQuote = currencyExchange.ParidadeCompra * currencyExchange.CotacaoVenda;
                convertedAmount = loanAmount * saleQuote;
            }
            else
            {
                throw new ArgumentException("Tipo de moeda inválido.");
            }

            return convertedAmount;
        }

        public async Task<decimal> CalculateTotalLoanAmountAsync(string selectedCurrencyType, decimal loanAmount, DateTime loanDate, DateTime endDate, decimal interestRate)
        {
            if (selectedCurrencyType == "BRL")
            {
                int totalMonths = CalculateTotalMonths(loanDate, endDate);
                decimal totalLoanAmount = loanAmount * (decimal)Math.Pow((double)(1 + interestRate), totalMonths);
                return totalLoanAmount;
            }
            else
            {
                decimal convertedLoanAmount = await ConvertCurrencyAsync(selectedCurrencyType, loanAmount, loanDate);
                int totalMonths = CalculateTotalMonths(loanDate, endDate);
                decimal totalLoanAmount = convertedLoanAmount * (decimal)Math.Pow((double)(1 + interestRate), totalMonths);
                return totalLoanAmount;
            }
        }

    }
}
