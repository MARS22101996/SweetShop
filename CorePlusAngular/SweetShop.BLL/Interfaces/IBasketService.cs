using System.Collections.Generic;
using SweetShop.BLL.Dto;
using SweetShop.DAL.Entities;

namespace SweetShop.BLL.Interfaces
{
   public interface IBasketService
   {
      void BuyProduct(OrderDetailsDto orderDetailsDto, string userId);

      OrderDetails GetOrderDetailsForProduct(int productId, int customerId);

      OrderDto GetBasketForUser(string userId);

      void Delete(int id);
   }
}
