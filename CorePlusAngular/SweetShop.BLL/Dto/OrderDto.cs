using System;
using System.Collections.Generic;
using SweetShop.CORE.Enums;

namespace SweetShop.BLL.Dto
{
   public class OrderDto
   {
      public int OrderId { get; set; }

      public int CustomerId { get; set; }

      public DateTime Date { get; set; }

      public OrderStatus PaymentState { get; set; }

      public DateTime? ShippedDate { get; set; }

      public decimal Sum { get; set; }

      public IEnumerable<OrderDetailsDto> OrderDetailses { get; set; }

      public CustomerDto Customer { get; set; }
   }
}
