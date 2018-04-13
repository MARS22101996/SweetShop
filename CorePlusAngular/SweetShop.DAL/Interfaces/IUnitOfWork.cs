using System.Threading.Tasks;
using SweetShop.DAL.Entities;

namespace SweetShop.DAL.Interfaces
{
   public interface IUnitOfWork
   {
      IProductRepository Products { get; }

      ICustomerRepository Customers { get; }

      IGenericRepository<Company> Companies { get; }

      IProductCustomerRepository ProductCustomers { get; }

      IOrderRepository Orders { get; }

      IGenericRepository<OrderDetails> OrderDetails { get; }

      IGenericRepository<Feedback> Feedbacks { get; }

      void Save();

      Task SaveChangesAsync();
   }
}
