using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SweetShop.DAL.Context;
using SweetShop.DAL.Entities;
using SweetShop.DAL.Interfaces;

namespace SweetShop.DAL.Repositories
{
   public class OrderRepository : GenericRepository<Order>, IOrderRepository
   {
      public OrderRepository(ApplicationContext context) : base(context)
      {
      }

      public Order GetOneWithDetails(Expression<Func<Order, bool>> predicate)
      {
         return Context.Set<Order>().Include(x => x.OrderDetailses).ThenInclude(x => x.Product)
         .FirstOrDefault(predicate);
      }
   }
}
