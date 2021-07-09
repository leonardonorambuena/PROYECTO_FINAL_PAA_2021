using PAA_MVC_2021.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PAA_MVC_2021.Models.ViewModels
{
    public class ProductCreateViewModel
    {
        public int? ProductId { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int? ProductPrice { get; set; }
        [Required]
        public int? ProducStock { get; set; }

        [Required]
        public int PlatformId { get; set; }

        
        public HttpPostedFileBase ProductFile { get; set; }

        public List<ProductPlatform> ProductPlatforms { get; set; }

        public Product Product { get; set; }
    }
}