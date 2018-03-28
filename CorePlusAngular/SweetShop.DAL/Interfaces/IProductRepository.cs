using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SweetShop.DAL.Entities;

namespace SweetShop.DAL.Interfaces
{
    public interface  IProductRepository : IGenericRepository<Product>
    {
        IEnumerable<Product> GetAllProducts();

        Product GetProduct(int id);
    }
}
