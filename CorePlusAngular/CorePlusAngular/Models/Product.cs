﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorePlusAngular.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }    // название 
        public string Company { get; set; } // производитель
        public decimal Price { get; set; }  // цена
    }
}
