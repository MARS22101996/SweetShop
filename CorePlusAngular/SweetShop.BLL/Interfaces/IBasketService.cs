using SweetShop.BLL.Dto;
using SweetShop.DAL.Entities;

namespace SweetShop.BLL.Interfaces
{
   public interface IBasketService
   {
      void BuyProduct(OrderDetailsDto orderDetailsDto, string userId);

      OrderDetails GetOrderDetailsForProduct(int productId, int customerId);
   }
}
