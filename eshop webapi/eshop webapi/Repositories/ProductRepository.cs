using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop_webapi.Contracts;
using eshop_webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace EshopApi.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private EshopApi_DBContext _context;

        public ProductRepository(EshopApi_DBContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public async Task<Product> Add(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Find(int id)
        {
            return await _context.Products.SingleOrDefaultAsync(c => c.ProductId == id);

        }

        public async Task<Product> Update(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Remove(int id)
        {
            var product = await _context.Products.SingleAsync(c => c.ProductId == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> IsExists(int id)
        {
            return await _context.Products.AnyAsync(c => c.ProductId == id);
        }
    }
}
