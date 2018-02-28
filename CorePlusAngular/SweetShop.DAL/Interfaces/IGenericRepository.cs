using System.Collections.Generic;
using SweetShop.DAL.Entities;

namespace SweetShop.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity Get(int id);

        void Create(TEntity product);

        void Update(TEntity product);

        void Delete(int id);
    }
}
