﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SweetShop.DAL.Entities
{
    public class AppUser : IdentityUser
    {
      // Extended Properties
      public string FirstName { get; set; }

      public string LastName { get; set; }

      public long? FacebookId { get; set; }

      public string PictureUrl { get; set; }
  }
}