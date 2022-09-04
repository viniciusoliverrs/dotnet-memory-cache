using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Product.API.Data.Context;
using Product.API.Domain.Data.Repositories;

namespace Product.API.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;
        
    private readonly IMemoryCache _memoryCache;
        public ProductRepository(ProductContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<Domain.Entities.Product> AddProductAsync(Domain.Entities.Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await GetProductAsync(id);
            if (product is null) throw new Exception("Product not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Domain.Entities.Product> GetProductAsync(Guid id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<List<Domain.Entities.Product>> GetProductsAsync()
        {
            var products = _memoryCache.GetOrCreate("products", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                entry.SlidingExpiration = TimeSpan.FromSeconds(15);
                entry.SetPriority(CacheItemPriority.High);
                System.Threading.Thread.Sleep(5000);
                return _context.Products.AsNoTracking().ToList();
            });
            return products;
        }

        public async Task<Domain.Entities.Product> UpdateProductAsync(Domain.Entities.Product product)
        {
            var model = await GetProductAsync(product.Id);
            if (model is null) throw new Exception("Product not found");

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}