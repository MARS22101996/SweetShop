namespace SweetShop.BLL.Dto
{
   public class ProductDto
   {
      public int Id { get; set; }

      public string Name { get; set; }

      public int CompanyId { get; set; }

      public decimal Price { get; set; }

      public string Description { get; set; }

      public int Likes { get; set; }

      public int Quantity { get; set; }

      public bool IsLikedByUser { get; set; }

      public int? CustomerProductId { get; set; }

      public CompanyDto Company { get; set; }
   }
}
