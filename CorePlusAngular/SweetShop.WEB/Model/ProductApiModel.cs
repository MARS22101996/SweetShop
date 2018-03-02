namespace SweetShop.WEB.Model
{
    public class ProductApiModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CompanyId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Likes { get; set; }
    }
}
