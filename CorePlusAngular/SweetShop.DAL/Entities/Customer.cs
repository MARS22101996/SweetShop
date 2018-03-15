using System;
using System.Collections.Generic;
using System.Text;

namespace SweetShop.DAL.Entities
{
    public class Customer
    {
      public int Id { get; set; }
      public string IdentityId { get; set; }
      public AppUser Identity { get; set; }
      public string Location { get; set; }
      public string Locale { get; set; }
      public string Gender { get; set; }

      public ICollection<ProductCustomer> ProductCustomers { get; set; }
   }
}
