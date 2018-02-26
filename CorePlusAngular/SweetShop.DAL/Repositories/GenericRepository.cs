using System.Collections.Generic;
using SweetShop.DAL.Context;
using SweetShop.DAL.Interfaces;

namespace SweetShop.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationContext _context;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            //if (!_context.Products.Any())
            //{
            //    _context.Companies.Add(new Company { Name = "Roshen", Description = "Roshen", HomePage = "http://test" });


            //    _context.Products.Add(new Product { Name = "iPhone X", Company = 1, Price = 79900 });
            //    _context.Products.Add(new Product { Name = "Galaxy S8", Company = 1, Price = 49900 });
            //    _context.Products.Add(new Product { Name = "Pixel 2", Company = 1, Price = 52900 });
            //    _context.SaveChanges();
            //}

            return _context.Set<TEntity>();
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

        public void  Update(TEntity entity)
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