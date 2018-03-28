using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SweetShop.DAL.Entities;

namespace SweetShop.DAL.Interfaces
{
    public interface  IProductCustomerRepository : IGenericRepository<ProductCustomer>
    {
        IEnumerable<ProductCustomer> GetWithProducts(Expression<Func<ProductCustomer, bool>> predicate);
    }
}
