using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SweetShop.DAL.Context;
using SweetShop.DAL.Entities;
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
            //    var company = new Company { Name = "Roshen", Description = "Roshen", HomePage = "http://test"};
            //    _context.Companies.Add(company);

            //    _context.Products.Add(new Product
            //    {
            //        Name = "Chocolate",
            //        Company = company,
            //        Price = 79900,
            //        CompanyId = company.Id
            //    });
            //    _context.Products.Add(new Product
            //    {
            //        Name = "Cake",
            //        Company = company,
            //        Price = 49900,
            //        CompanyId = company.Id
            //    });
            //    _context.Products.Add(new Product
            //    {
            //        Name = "Sweets",
            //        Company = company,
            //        Price = 52900,
            //        CompanyId = company.Id
            //    });

            //    _context.SaveChanges();
            //}

            return _context.Set<TEntity>();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Set<Product>().Include(x => x.Company);
        }

        public Product GetProduct(int id)
        {
            return _context.Set<Product>().Include(x => x.Company).FirstOrDefault(x => x.Id == id);
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