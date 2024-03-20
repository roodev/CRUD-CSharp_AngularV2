using Microsoft.EntityFrameworkCore.Diagnostics;
using CustomerLoan.API.Models;
using CustomerLoan.API.Repository;

namespace CustomerLoan.API.Services
{
    public class CustomerServices : IService<Customer>
    {
        private readonly IRepository customerRepository;
        public CustomerServices(IRepository _customerRepository) 
        {
            customerRepository = _customerRepository;
        }
        private bool IsCPFUnique(string cpf)
        {
            return !customerRepository.GetAllCustomers().Any(c => c.CPF == cpf);
        }

        public bool Add(Customer register) 
        {
            if (register == null) throw new ArgumentNullException(nameof(register), "Registro nulo");
            if (!IsCPFUnique(register.CPF)) throw new InvalidOperationException("CPF já cadastrado.");
            customerRepository.AddCustomer(register);
            return customerRepository.SaveChanges();
        }

        public bool Delete(int id) 
        {
            if (id == 0 || id == null) throw new ArgumentNullException("Id não válido");
            customerRepository.DeleteCustomer(id);
            return customerRepository.SaveChanges();
        }

        public IEnumerable<Customer> List() 
        {
            return customerRepository.GetAllCustomers();
        }

        public bool Update(Customer register) 
        {
            if (register == null) throw new ArgumentNullException("Registro nulo");
            if (register.Id == 0 || register.Id == null) throw new ArgumentNullException("Id não válido");
            customerRepository.UpdateCustomer(register);
            return customerRepository.SaveChanges();
        }

        public Customer GetCustomerById(int id)
        {
            return customerRepository.GetCustomerById(id);
        }

    }
}
