using GeekShopping_ProductAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping_ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
