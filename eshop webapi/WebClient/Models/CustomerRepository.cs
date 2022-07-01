using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace WebClient.Models
{
    public class CustomerRepository : ICustomerRepository
    {
        private string apiUrl = "http://localhost:3962/api/customers";
        private HttpClient _client;
        public CustomerRepository()
        {
            _client = new HttpClient();
        }

        public List<Customer> GetAllCustomer(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var result = _client.GetStringAsync(apiUrl).Result;
            List<Customer> list = JsonConvert.DeserializeObject<List<Customer>>(result);
            return list;
        }

        public Customer GetCustomerById(int customerId)
        {
            var result = _client.GetStringAsync(apiUrl + "/" + customerId).Result;
            Customer customer = JsonConvert.DeserializeObject<Customer>(result);

            return customer;
        }

        public HttpResponseMessage AddCustomer(Customer customer)
        {
            string jsonCustomer = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

            var result = _client.PostAsync(apiUrl, content).Result;
            return result;
        }

        public HttpResponseMessage UpdateCustomer(Customer customer)
        {
            string jsonCustomer = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

            var result = _client.PutAsync(apiUrl+"/"+customer.CustomerId, content).Result;
            return result;
        }

        public HttpResponseMessage DeleteCustomer(int customerId)
        {
            var result = _client.DeleteAsync(apiUrl + "/" + customerId).Result;
            return result;

        }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

    }
}
