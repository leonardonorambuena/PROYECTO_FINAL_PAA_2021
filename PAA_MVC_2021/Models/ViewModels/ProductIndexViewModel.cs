using PAA_MVC_2021.Models.Entities;
using System.Collections.Generic;

namespace PAA_MVC_2021.Models.ViewModels
{
    public class ProductIndexViewModel
    {
        public List<Product> Products { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public int? PlatformId { get; set; } // esto por defecto es null

        public int Sort { get; set; } // esto por defecto es 0

        public List<ProductPlatform> Platforms { get; set; }


    }
}