using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SweetShop.DAL.Entities;

namespace SweetShop.DAL.Interfaces
{
   public interface ICustomerRepository : IGenericRepository<Customer>
   {
     Task<Customer> GetByUserId(string id);
   }
}
