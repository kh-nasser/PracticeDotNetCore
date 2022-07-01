using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop_webapi.Contracts;
using eshop_webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace EshopApi.Repositories
{
    public class SalesPersonsRepository : ISalesPersonsRepository
    {
        private EshopApi_DBContext _dbContext;

        public SalesPersonsRepository(EshopApi_DBContext context)
        {
            _dbContext = context;
        }

        public IEnumerable<SalePerson> GetAll()
        {
            return _dbContext.SalePeople.ToList();
        }

        public async Task<SalePerson> Add(SalePerson sales)
        {
            await _dbContext.SalePeople.AddAsync(sales);
            await _dbContext.SaveChangesAsync();
            return sales;
        }

        public async Task<SalePerson> Find(int id)
        {
            return await _dbContext.SalePeople.SingleOrDefaultAsync(s => s.SalePersonId == id);

        }

        public async Task<SalePerson> Update(SalePerson sales)
        {
            _dbContext.Update(sales);
            await _dbContext.SaveChangesAsync();
            return sales;
        }

        public async Task<SalePerson> Remove(int id)
        {
            var sales = await _dbContext.SalePeople.SingleAsync(s => s.SalePersonId == id);
            _dbContext.SalePeople.Remove(sales);
            await _dbContext.SaveChangesAsync();
            return sales;
        }

        public async Task<bool> IsExists(int id)
        {
            return await _dbContext.SalePeople.AnyAsync(s => s.SalePersonId == id);
        }
    }
}
