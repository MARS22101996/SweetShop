namespace SweetShop.BLL.Dto
{
    public class OrderDetailsDto
    {
       public int OrderDetailsId { get; set; }

       public decimal Price { get; set; }

       public int Quantity { get; set; }

       public float Discount { get; set; }

       public int OrderId { get; set; }

       public int ProductId { get; set; }

       public ProductDto Product { get; set; }

       public OrderDto Order { get; set; }
   }
}