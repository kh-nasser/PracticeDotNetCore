using System.Collections.Generic;
using System.Net.Http;

namespace WebClient.Models
{
    public interface ICustomerRepository
    {
        List<Customer> GetAllCustomer(string token);
        Customer GetCustomerById(int customerId);
        HttpResponseMessage AddCustomer(Customer customer);
        HttpResponseMessage UpdateCustomer(Customer customer);
        HttpResponseMessage DeleteCustomer(int customerId);
    }
}