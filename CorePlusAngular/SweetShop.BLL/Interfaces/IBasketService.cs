using System;
using System.Collections.Generic;
using System.Text;
using SweetShop.BLL.Dto;

namespace SweetShop.BLL.Interfaces
{
    public interface IBasketService
    {
       void BuyProduct(OrderDetailsDto orderDetailsDto, string userId);
    }
}
