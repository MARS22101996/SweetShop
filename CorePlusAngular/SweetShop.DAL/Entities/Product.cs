using System.ComponentModel.DataAnnotations.Schema;

namespace SweetShop.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Company")]
        public int Company { get; set; }

        public decimal Price { get; set; }
    }
}
