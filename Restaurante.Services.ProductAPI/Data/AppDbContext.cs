using Microsoft.EntityFrameworkCore;
using Restaurante.Services.ProductAPI.Models;
namespace Restaurante.Services.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
          
        }


        public DbSet<Product> Coupons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }
    }
}
