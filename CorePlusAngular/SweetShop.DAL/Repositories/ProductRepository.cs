using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SweetShop.DAL.Context;
using SweetShop.DAL.Entities;
using SweetShop.DAL.Interfaces;

namespace SweetShop.DAL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext context)
            : base(context)
        {

        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Set<Product>().Include(x => x.Company);
        }

        public Product GetProduct(int id)
        {
            return _context.Set<Product>().Include(x => x.Company).FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<Product> Get(Expression<Func<Product, bool>> predicate)
        {
            return _context.Set<Product>().Include(x => x.Company).Where(predicate);
        }
    }
}
