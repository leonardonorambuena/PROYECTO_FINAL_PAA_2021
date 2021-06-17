using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PAA_MVC_2021.Models.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}