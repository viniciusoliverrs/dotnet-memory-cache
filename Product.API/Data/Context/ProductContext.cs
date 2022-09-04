using Microsoft.EntityFrameworkCore;
namespace Product.API.Data.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<Domain.Entities.Product> Products { get; set; }
    }
}