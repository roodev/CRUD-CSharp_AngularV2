using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CustomerLoan.API.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }

        public ICollection<Loan> Loans { get; set; }

        public Customer()
        {
            Loans = new List<Loan>();
        }

        public Customer(string name, string cpf) 
        {
            Name = name;
            CPF = cpf;
            Loans = new List<Loan>();
        }
    }
}
