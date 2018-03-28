using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SweetShop.DAL.Context;
using SweetShop.DAL.Interfaces;

namespace SweetShop.DAL.Repositories
{
   public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
   {
      protected readonly ApplicationContext Context;

      public GenericRepository(ApplicationContext context)
      {
         Context = context;
      }

      public virtual IEnumerable<TEntity> GetAll()
      {
         return Context.Set<TEntity>();
      }

      public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
      {
         return Context.Set<TEntity>().Where(predicate);
      }

      public virtual TEntity Get(int id)
      {
         var entity = Context.Set<TEntity>().Find(id);

         return entity;
      }

      public virtual void Create(TEntity entity)
      {
         Context.Set<TEntity>().Add(entity);
      }

      public virtual void Update(TEntity entity)
      {
         Context.Set<TEntity>().Update(entity);
      }

      public virtual void Delete(int id)
      {
         var entity = Context.Set<TEntity>().Find(id);

         if (entity != null)
         {
            Context.Set<TEntity>().Remove(entity);
         }
      }
   }
}