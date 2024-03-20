using Microsoft.EntityFrameworkCore;
using CustomerLoan.API.Models;

namespace CustomerLoan.API.Repository
{
    public class CustomerLoanContext : DbContext
    {
        public CustomerLoanContext(DbContextOptions<CustomerLoanContext> options) : base(options) 
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Customer)
                .WithMany(c => c.Loans)
                .HasForeignKey(l => l.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
