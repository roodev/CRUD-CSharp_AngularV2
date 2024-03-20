using CustomerLoan.API.Models;

namespace CustomerLoan.API.Models.DTO
{
    public class CreateLoansDTO
    {
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public decimal ConversionRate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int MonthsToDueDate { get; set; }
        public int CustomerId { get; set; }
    }
}
