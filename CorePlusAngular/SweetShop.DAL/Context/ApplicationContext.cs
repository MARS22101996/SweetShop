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
  }
}
