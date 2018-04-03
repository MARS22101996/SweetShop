using System.ComponentModel.DataAnnotations;

namespace SweetShop.WEB.Model
{
   public class OrderDetailsApiModel
   {
      public int OrderDetailsId { get; set; }

      [Range(0, 10000, ErrorMessage = "Price should be in the range 0 - 10000")]
      public decimal Price { get; set; }

      [Range(0, 100, ErrorMessage = "Price should be in the range 0 - 100")]
      public int Quantity { get; set; }

      [Range(0.0, 100.0, ErrorMessage = "Discount should be the range 0 - 100")]
      public float Discount { get; set; }

      public int OrderId { get; set; }

      public int ProductId { get; set; }

      public ProductViewApiModel Product { get; set; }
   }
}
