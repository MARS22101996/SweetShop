using System;
using System.Collections.Generic;
using System.Linq;
using CorePlusAngular.Models;
using Microsoft.AspNetCore.Mvc;

namespace CorePlusAngular.Controllers
{
    [Route("api/products")]
    public class ProductController : Controller
    {
        ApplicationContext _db;
        public ProductController(ApplicationContext context)
        {
            _db = context;
            if (!_db.Products.Any())
            {
                _db.Products.Add(new Product { Name = "iPhone X", Company = "Apple", Price = 79900 });
                _db.Products.Add(new Product { Name = "Galaxy S8", Company = "Samsung", Price = 49900 });
                _db.Products.Add(new Product { Name = "Pixel 2", Company = "Google", Price = 52900 });
                _db.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _db.Products.ToList();
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            Product product = _db.Products.FirstOrDefault(x => x.Id == id);
            return product;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Update(product);
                _db.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _db.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
            }
            return Ok(product);
        }
    }
}