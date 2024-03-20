using CustomerLoan.API.Models;

namespace CustomerLoan.API.Repository
{
    public interface IRepository
    {
        void AddCustomer(Customer customer);
        bool SaveChanges();
        void DeleteCustomer(int id);
        void UpdateCustomer(Customer customer);
        Customer GetCustomerById(int id);

        IEnumerable<Customer> GetAllCustomers();
    }
}
