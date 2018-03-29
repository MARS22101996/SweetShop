using System;
using System.Linq.Expressions;
using SweetShop.DAL.Entities;

namespace SweetShop.DAL.Interfaces
{
   public interface IOrderRepository : IGenericRepository<Order>
   {
      Order GetOneWithDetails(Expression<Func<Order, bool>> predicate);
   }
}
