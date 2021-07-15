using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped]
        public int CountProducts { get; set; }
    }

}