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
   public class ProductCustomerRepository : GenericRepository<ProductCustomer>, IProductCustomerRepository
   {
      public ProductCustomerRepository(ApplicationContext context)
         : base(context)
      {
      }

      public IEnumerable<ProductCustomer> GetWithProducts(Expression<Func<ProductCustomer, bool>> predicate)
      {
         return _context.Set<ProductCustomer>().Include(x => x.Product).ThenInclude(x => x.Company).Where(predicate);
      }
   }
}
