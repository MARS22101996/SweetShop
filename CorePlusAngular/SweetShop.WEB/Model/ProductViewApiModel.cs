namespace SweetShop.WEB.Model
{
   public class ProductViewApiModel
   {
      public int Id { get; set; }

      public string Name { get; set; }

      public string Company { get; set; }

      public int CompanyId { get; set; }

      public string Description { get; set; }

      public int Quantity { get; set; }

      public decimal Price { get; set; }

      public int Likes { get; set; }

      public bool IsLikedByUser { get; set; }
   }
}
