using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PAA_MVC_2021.Models.Entities
{
    public class ProductPlatform
    {
        [Key]
        public int ProductPlatformId { get; set; }

        [Required]
        public string ProductPlatformName { get; set; }
        [Required]
        public string ProductPlatformLogo { get; set; }

        public List<Product> Products { get; set; }

        public int CountProducst => Products != null ? Products.Count : 0;
    }

}