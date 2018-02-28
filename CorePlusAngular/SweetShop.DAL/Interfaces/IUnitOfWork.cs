using SweetShop.DAL.Entities;

namespace SweetShop.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }

        IGenericRepository<Company> Companies { get; }

        void Save();
    }
}
