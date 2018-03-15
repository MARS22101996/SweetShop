using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SweetShop.DAL.Entities
{
    public class Product
    {
        public Product()
        {
            Likes = 0;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Likes { get; set; }

        public virtual Company Company { get; set; }

        public ICollection<ProductCustomer> ProductCustomers { get; set; }
   }
}
