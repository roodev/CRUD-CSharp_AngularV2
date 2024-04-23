    using Microsoft.AspNetCore.Mvc;
    using CustomerLoan.API.Models;
    using CustomerLoan.API.Repository;
    using CustomerLoan.API.Services;
    using CustomerLoan.API.Models.DTO;

    namespace CustomerLoan.API.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class LoanController : ControllerBase
        {
            CustomerLoanContext _ctx;
            private readonly ILoanService<Loan> _service;
            private readonly IDollarValueRepository _dollarValueRepository;
            public LoanController(ILoanService<Loan> service, IDollarValueRepository dollarValueRepository)
            {
                _service = service;
                _dollarValueRepository = dollarValueRepository;
            }

            [HttpGet("getloans")]
            public IActionResult GetAllLoans()
            {
                var loanServices = _service.List();
                return Ok(loanServices);
            }

            [HttpGet("getloanbyid/{id}")]
            public IActionResult GetLoanById(int id)
            {
                var loanService = _service.GetLoanById(id);
                return Ok(loanService);
            }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateLoan([FromBody] CalculateLoanDTO calculateLoanDTO)
        {
            if (calculateLoanDTO == null || string.IsNullOrEmpty(calculateLoanDTO.CurrencyType) || calculateLoanDTO.LoanAmount <= 0 || calculateLoanDTO.DueDate == default)
            {
                return BadRequest("Dados inválidos.");
            }

            try
            {
                int totalMonths = _service.CalculateTotalMonths(DateTime.UtcNow, calculateLoanDTO.DueDate);
                decimal dollarValue = await _service.GetDollarValueAsync(DateTime.UtcNow);
                if (calculateLoanDTO.CurrencyType != "BRL") { decimal convertedAmount = await _service.ConvertCurrencyAsync(calculateLoanDTO.CurrencyType, calculateLoanDTO.CurrencySymbol, calculateLoanDTO.LoanAmount, DateTime.UtcNow); }
                decimal totalLoanAmount = await _service.CalculateTotalLoanAmountAsync(calculateLoanDTO.CurrencyType,calculateLoanDTO.CurrencySymbol, calculateLoanDTO.LoanAmount, DateTime.UtcNow, calculateLoanDTO.DueDate, 0.05m);

             
                Loan loan = new Loan
                {
                    Amount = calculateLoanDTO.LoanAmount,
                    Currency = calculateLoanDTO.CurrencySymbol,
                    DueDate = calculateLoanDTO.DueDate,
                    TotalAmount = totalLoanAmount, 
                    MonthsToDueDate = totalMonths 
                };

                return Ok(loan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao calcular o empréstimo: {ex.Message}");
            }
        }

        [HttpPost("createloan")]
        public IActionResult AddLoan([FromBody] CreateLoansDTO loansDTO)
            {
                Loan loan = new Loan();
                loan.LoanDate = DateTime.UtcNow;
                loan.Currency = loansDTO.Currency;
                loan.Amount = loansDTO.Amount;
                loan.DueDate = loansDTO.DueDate;
                loan.TotalAmount = loansDTO.TotalAmount;
                loan.MonthsToDueDate = loansDTO.MonthsToDueDate;
                loan.CustomerId = loansDTO.CustomerId;
                return Ok(_service.Add(loan));
            }

        [HttpDelete("{id}")]
        public IActionResult DeleteLoan(int id)
            {
                return Ok(_service.Delete(id));
            }

            [HttpPut]
            public IActionResult UpdateLoan([FromBody] Loan loan)
            {
                return Ok(_service.Update(loan));
            }
        }
    }
