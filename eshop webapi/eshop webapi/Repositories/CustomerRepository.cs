using eshop_webapi.Contracts;
using eshop_webapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop_webapi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private EshopApi_DBContext _dbContext;
        private IMemoryCache _cache;

        public CustomerRepository(EshopApi_DBContext dbContext, IMemoryCache cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }

        public async Task<Customer> Add(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<int> CountCustomer()
        {
            return await _dbContext.Customers.CountAsync();
        }

        public async Task<Customer> Find(int id)
        {
            var cacheCustomer = _cache.Get<Customer>(id);
            //check cache for key:id
            if (cacheCustomer != null)
            {
                //key found in memory-cache
                return cacheCustomer;
            }
            else
            {
                //key not found in memory-cache

                //fetch from db
                var customer = await _dbContext.Customers.Include(c => c.Orders).SingleOrDefaultAsync(c => c.CustomerId == id);

                //cache key in memory-cache
                var cacheOption = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
                _cache.Set(customer.CustomerId, cacheCustomer, cacheOption);
                return customer;
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            return _dbContext.Customers.ToList();
        }

        public async Task<bool> IsExist(int id)
        {
            return await _dbContext.Customers.AnyAsync(c => c.CustomerId == id);
        }

        public async Task<Customer> Remove(int id)
        {
            var customer = await _dbContext.Customers.SingleAsync(c => c.CustomerId == id);
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Update(Customer customer)
        {
            _dbContext.Update(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }
    }
}
