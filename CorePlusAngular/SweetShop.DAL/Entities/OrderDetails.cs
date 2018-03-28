using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SweetShop.DAL.Entities
{
   public class OrderDetails
   {
      [Key]
      public int OrderDetailsId { get; set; }

      [Required]
      public decimal Price { get; set; }

      [Required]
      public int Quantity { get; set; }

      public float Discount { get; set; }

      [ForeignKey("Order")]
      public int OrderId { get; set; }

      [Required]
      [ForeignKey("Product")]
      public int ProductId { get; set; }

      public virtual Product Product { get; set; }

      public virtual Order Order { get; set; }
   }
}
