using System.ComponentModel.DataAnnotations.Schema;

namespace SweetShop.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public virtual Company Company { get; set; }
    }
}
