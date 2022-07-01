using eshop_webapi.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eshop_webapi.Contracts
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        Task<Customer> Add(Customer customer);
        Task<Customer> Find(int id);
        Task<Customer> Update(Customer customer);
        Task<Customer> Remove(int id);
        Task<bool> IsExist(int id);
        Task<int> CountCustomer();
    }
}