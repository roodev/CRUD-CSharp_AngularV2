using Microsoft.EntityFrameworkCore;

namespace CustomerLoan.API.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public DateTime LoanDate { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public decimal ConversionRate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalAmount { get; set; } 
        public int MonthsToDueDate { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }


        public Loan()
        {
            LoanDate = DateTime.Now;
        }

        public Loan(DateTime loanDate, string currency, decimal amount, decimal conversionRate, DateTime dueDate, decimal totalAmount, int monthsToDueDate, int customerId)
        {
            LoanDate = loanDate;
            Currency = currency;
            Amount = amount;
            ConversionRate = conversionRate;
            DueDate = dueDate;
            TotalAmount = totalAmount;
            MonthsToDueDate = monthsToDueDate;
            CustomerId = customerId;
        }
    }
}
