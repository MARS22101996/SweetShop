﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SweetShop.DAL.Context;
using SweetShop.DAL.Interfaces;

namespace SweetShop.DAL.Repositories
{
   public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
   {
      protected readonly ApplicationContext _context;

      public GenericRepository(ApplicationContext context)
      {
         _context = context;
      }

      public IEnumerable<TEntity> GetAll()
      {
         return _context.Set<TEntity>();
      }

      public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
      {
         return _context.Set<TEntity>().Where(predicate);
      }

      public TEntity Get(int id)
      {
         var entity = _context.Set<TEntity>().Find(id);

         return entity;
      }

      public void Create(TEntity entity)
      {
         _context.Set<TEntity>().Add(entity);
      }

      public void Update(TEntity entity)
      {
         _context.Set<TEntity>().Update(entity);
      }

      public void Delete(int id)
      {
         var entity = _context.Set<TEntity>().Find(id);

         if (entity != null)
         {
            _context.Set<TEntity>().Remove(entity);
         }
      }
   }
}