using CustomerLoan.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace CustomerLoan.API.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly CustomerLoanContext _context;

        public LoanRepository(CustomerLoanContext context)
        {
            _context = context;
        }

        public void AddLoan(Loan loan)
        {
            if (loan.CustomerId <= 0)
            {
                throw new ArgumentException("CustomerId inválido", nameof(loan.CustomerId));
            }

            var existingCustomer = _context.Customers.Find(loan.CustomerId);
            if (existingCustomer == null)
            {
                throw new ArgumentException("Cliente não encontrado", nameof(loan.CustomerId));
            }

            loan.Customer = existingCustomer;
            _context.Add(loan);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public IEnumerable<Loan> GetAllLoans()
        {
            IQueryable<Loan> query = _context.Loans.Include(l => l.Customer);
            return query.ToList();
        }

        public void DeleteLoan(int id)
        {
            IQueryable<Loan> query = _context.Loans;
            if (id == null) throw new ArgumentNullException("Id nulo");

            Loan loan = query.Where(l => l.Id == id).FirstOrDefault();
            if (loan == null) throw new ArgumentNullException("Empréstimo não encontrado");
            _context.Remove(loan);
        }

        public void UpdateLoan(Loan entity)
        {
            IQueryable<Loan> query = _context.Loans;
            if (entity.Id == null) throw new ArgumentNullException("Id nulo");
            Loan loan = query.Where(l => l.Id == entity.Id).FirstOrDefault();
            loan.LoanDate = entity.LoanDate;
            loan.Currency = entity.Currency;
            loan.Amount = entity.Amount;
            loan.ConversionRate = entity.ConversionRate;
            loan.DueDate = entity.DueDate;
            loan.TotalAmount = entity.TotalAmount;
            loan.MonthsToDueDate = entity.MonthsToDueDate;
            loan.CustomerId = entity.CustomerId;
            _context.Update(loan);
        }

        public Loan GetLoanById(int Id)
        {
            IQueryable<Loan> query = _context.Loans;
            return query.Where(c => c.Id == Id).FirstOrDefault();
        }

    }
}
