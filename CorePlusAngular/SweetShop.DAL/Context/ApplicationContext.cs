using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SweetShop.DAL.Entities;

namespace SweetShop.DAL.Context
{
   public class ApplicationContext : IdentityDbContext<AppUser>
   {
      public ApplicationContext(DbContextOptions options)
         : base(options)
      {
      }

      public DbSet<Product> Products { get; set; }

      public DbSet<Company> Companies { get; set; }

      public DbSet<Customer> Customers { get; set; }

      public DbSet<ProductCustomer> ProductCustomers { get; set; }

      public DbSet<Order> Orders { get; set; }

      public DbSet<OrderDetails> OrderDetails { get; set; }

      public DbSet<Feedback> Feedbacks { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<ProductCustomer>()
         .HasOne(p => p.Product)
         .WithMany(b => b.ProductCustomers)
         .OnDelete(DeleteBehavior.Cascade);
      }
   }
}
