using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SweetShop.DAL.Entities;

namespace SweetShop.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
       IEnumerable<TEntity> GetAll();

       IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

       TEntity GetOne(Expression<Func<TEntity, bool>> predicate);

       TEntity Get(int id);

       void Create(TEntity product);

       void Update(TEntity product);

       void Delete(int id);
    }
}
