using Microsoft.EntityFrameworkCore;
using SweetShop.DAL.Entities;

namespace SweetShop.DAL.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
