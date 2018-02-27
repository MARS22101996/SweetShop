using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweetShop.WEB.Model
{
    public class ProductViewApiModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Company { get; set; }

        public decimal Price { get; set; }
    }
}
