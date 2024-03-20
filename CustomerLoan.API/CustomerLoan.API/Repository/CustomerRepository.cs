using CustomerLoan.API.Models;
using System.Transactions;

namespace CustomerLoan.API.Repository
{
    public class CustomerRepository : IRepository
    {
        private readonly CustomerLoanContext _context;

        public CustomerRepository(CustomerLoanContext context)
        {
            _context = context;
        }

        public void AddCustomer(Customer entity)
        {
            _context.Add(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            IQueryable<Customer> query = _context.Customers;
            return query.ToList();
        }

        public void DeleteCustomer(int id)
        {
            IQueryable<Customer> query = _context.Customers;
            if (id == null) throw new ArgumentNullException("Id nulo");

            Customer customer = query.Where(c => c.Id == id).FirstOrDefault();
            if (customer == null) throw new ArgumentNullException("Cliente não encontrado");
            _context.Remove(customer);
        }

        public void UpdateCustomer(Customer entity)
        {
            IQueryable<Customer> query = _context.Customers;
            if (entity.Id == null) throw new ArgumentNullException("Id nulo");
            Customer customer = query.Where(c => c.Id == entity.Id).FirstOrDefault();
            customer.Name = entity.Name;
            _context.Update(customer);
        }

        public Customer GetCustomerById(int Id)
        {
            IQueryable<Customer> query = _context.Customers;
            return query.Where(c => c.Id == Id).FirstOrDefault();
        }
    }
}
