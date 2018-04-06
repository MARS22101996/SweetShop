using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SweetShop.CORE.Enums;

namespace SweetShop.DAL.Entities
{
   public class Order
   {
      public Order()
      {
         PaymentState = OrderStatus.New;
      }

      [Key]
      public int OrderId { get; set; }

      [Required]
      [ForeignKey("Customer")]
      public int CustomerId { get; set; }

      [Required]
      public DateTime Date { get; set; }

      [Required]
      public OrderStatus PaymentState { get; set; }

      public DateTime? ShippedDate { get; set; }

      public decimal Sum { get; set; }

      public virtual ICollection<OrderDetails> OrderDetailses { get; set; }

      public virtual Customer Customer { get; set; }
   }
}
