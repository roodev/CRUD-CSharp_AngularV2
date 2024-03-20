using Microsoft.AspNetCore.Mvc;
using CustomerLoan.API.Models;
using CustomerLoan.API.Repository;
using CustomerLoan.API.Services;
using CustomerLoan.API.Models.DTO;

namespace CustomerLoan.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        CustomerLoanContext _ctx;
        private readonly IService<Customer> _service;
        public CustomerController(IRepository repository) 
        {
            _service = new CustomerServices(repository);
        }

        [HttpGet("getcustomers")]
        public IActionResult GetAllCustomers()
        {
            var customerServices = _service.List();
            return Ok(customerServices);
        }

        [HttpGet("getcustomerbyid/{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customerService = _service.GetCustomerById(id);
            return Ok(customerService);
        }


        [HttpPost]
        public IActionResult AddCustomer([FromBody] CreateCustomersDTO customerDTO)
        {
            Customer customer = new Customer();
            customer.Name = customerDTO.Name;
            customer.CPF = customerDTO.CPF;
            return Ok(_service.Add(customer));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            return Ok(_service.Delete(id));
        }

        [HttpPut]
        public IActionResult UpdateCustomer([FromBody] Customer customer)
        {
            return Ok(_service.Update(customer));
        }
    }
}
