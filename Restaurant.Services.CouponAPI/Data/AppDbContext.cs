using Microsoft.EntityFrameworkCore;
using Restaurant.Services.CouponAPI.Models;

namespace Restaurant.Services.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
          
        }


        public DbSet<Coupon> Coupons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "100F",
                DiscountAmount = 10,
                MinAmount = 10,
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode = "200F",
                DiscountAmount = 20,
                MinAmount = 40,
            });
        }
    }
}
