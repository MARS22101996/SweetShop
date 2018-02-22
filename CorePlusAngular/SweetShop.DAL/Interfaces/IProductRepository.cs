using System.Collections.Generic;
using SweetShop.DAL.Entities;

namespace SweetShop.DAL.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();

        Product Get(int id);

        void Create(Product product);

        void Update(Product product);

        void Delete(int id);
    }
}
