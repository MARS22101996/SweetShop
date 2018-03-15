using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SweetShop.DAL.Entities
{
    public class ProductCustomer
    {
       public int Id { get; set; }

       [ForeignKey("Product")]
       public int? ProductId { get; set; }

       public virtual Product Product { get; set; }

       [ForeignKey("Customer")]
       public int? CustomerId { get; set; }

       public virtual Customer Customer { get; set; }
    }
}
