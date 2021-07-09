using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PAA_MVC_2021.Models.ViewModels
{
    public class PlatformCreateViewModel
    {
        public int ProductPlatformId { get; set; }

        [Required]
        public string ProductPlatformName { get; set; }
        [Required]
        public HttpPostedFileBase PlatformFile { get; set; }
    }
}