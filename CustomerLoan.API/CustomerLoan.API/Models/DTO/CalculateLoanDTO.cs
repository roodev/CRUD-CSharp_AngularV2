namespace CustomerLoan.API.Models.DTO
{
    public class CalculateLoanDTO
    {
        public string CurrencyType { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal LoanAmount { get; set; }
        public DateTime DueDate { get; set; }
    }
}
