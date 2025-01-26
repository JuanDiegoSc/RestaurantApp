using Microsoft.EntityFrameworkCore;
using Restaurant.Services.RewardAPI.Models;
namespace Restaurant.Services.RewardAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
          
        }

        public DbSet<Rewards> Rewards { get; set; }
     
    }
}
