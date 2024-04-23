namespace CustomerLoan.API.Services
{
    public interface ILoanService<T>
    {
        bool Add(T register);
        bool Delete(int id);
        bool Update(T register);
        T GetLoanById(int id);
        IEnumerable<T> List();
        int CalculateTotalMonths(DateTime startDate, DateTime endDate);
         Task<decimal> GetDollarValueAsync(DateTime date);
        Task<decimal> ConvertCurrencyAsync(string selectedCurrencyType, string selectedCurrencySymbol,  decimal loanAmount, DateTime loanDate);
        Task<decimal> CalculateTotalLoanAmountAsync(string selectedCurrencyType, string selectedCurrencySymbol, decimal loanAmount, DateTime loanDate, DateTime endDate, decimal interestRate);
    }
}
