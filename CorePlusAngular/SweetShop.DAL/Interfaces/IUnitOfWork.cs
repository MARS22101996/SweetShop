using SweetShop.DAL.Entities;

namespace SweetShop.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Product> Products { get; }

        IGenericRepository<Company> Companies { get; }

        void Save();
    }
}
