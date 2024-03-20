using CustomerLoan.API.Models;

namespace CustomerLoan.API.Repository
{
    public interface ILoanRepository
    {
        void AddLoan(Loan loan);
        bool SaveChanges();
        void DeleteLoan(int id);
        void UpdateLoan(Loan loan);
        Loan GetLoanById(int id);

        IEnumerable<Loan> GetAllLoans();
    }
}
