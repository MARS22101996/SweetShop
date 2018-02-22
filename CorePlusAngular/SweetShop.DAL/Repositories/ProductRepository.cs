using System.Collections.Generic;
using System.Linq;
using SweetShop.DAL.Context;
using SweetShop.DAL.Entities;
using SweetShop.DAL.Interfaces;

namespace SweetShop.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            if (!_context.Products.Any())
            {
                _context.Products.Add(new Product {Name = "iPhone X", Company = "Apple", Price = 79900});
                _context.Products.Add(new Product {Name = "Galaxy S8", Company = "Samsung", Price = 49900});
                _context.Products.Add(new Product {Name = "Pixel 2", Company = "Google", Price = 52900});
                _context.SaveChanges();
            }

            return _context.Products;
        }

        public Product Get(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);

            return product;
        }

        public void Create(Product product)
        {
            _context.Products.Add(product);
        }

        public void  Update(Product product)
        {
            _context.Update(product);
        }

        public void Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
        }
    }
}