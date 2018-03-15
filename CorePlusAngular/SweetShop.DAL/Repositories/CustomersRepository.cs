using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SweetShop.DAL.Context;
using SweetShop.DAL.Entities;
using SweetShop.DAL.Interfaces;

namespace SweetShop.DAL.Repositories
{
   public class CustomersRepository : GenericRepository<Customer>, ICustomerRepository
   {
      public CustomersRepository(ApplicationContext context)
         : base(context)
      {
      }

      public async Task<Customer> GetByUserId(string id)
      {
         var customer = await _context.Customers.Include(c => c.Identity)
         .SingleAsync(c => c.Identity.Id == id);

         return customer;
      }

      public async Task CreateAsync(Customer customer)
      {
         await _context.Customers.AddAsync(customer);
      }
   }
}
