using SweetShop.CORE.Enums;
using System;
using System.Collections.Generic;

namespace SweetShop.WEB.Model
{
   public class BasketApiModel
   {
      public DateTime Date { get; set; }

      public OrderStatus PaymentState { get; set; }

      public DateTime? ShippedDate { get; set; }

      public decimal Sum { get; set; }

      public IEnumerable<OrderDetailsApiModel> OrderDetails;
   }
}
