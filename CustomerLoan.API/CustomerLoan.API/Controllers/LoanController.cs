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

            [HttpPost]
            public IActionResult AddLoan([FromBody] CreateLoansDTO loansDTO)
            {
                Loan loan = new Loan();
                loan.LoanDate = DateTime.UtcNow;
                loan.Currency = loansDTO.Currency;
                loan.Amount = loansDTO.Amount;
                loan.ConversionRate = loansDTO.ConversionRate;
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
