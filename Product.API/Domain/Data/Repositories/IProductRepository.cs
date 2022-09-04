using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Domain.Data.Repositories
{
    public interface IProductRepository
    {
        Task<List<Domain.Entities.Product>> GetProductsAsync();
        Task<Domain.Entities.Product> GetProductAsync(Guid id);
        Task<Domain.Entities.Product> AddProductAsync(Domain.Entities.Product product);
        Task<Domain.Entities.Product> UpdateProductAsync(Domain.Entities.Product product);
        Task<bool> DeleteProductAsync(Guid id);
    }
}