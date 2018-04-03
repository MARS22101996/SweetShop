using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SweetShop.DAL.Entities
{
   [Table("AspNetUsers")]
   public class AppUser : IdentityUser
    {
       public string FirstName { get; set; }

       public string LastName { get; set; }

       public long? FacebookId { get; set; }

       public string PictureUrl { get; set; }
   }
}
