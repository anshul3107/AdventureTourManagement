using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADSM.Models
{
    public class Packages
    {
        [Key]
        public int package_id { get; set; }
        public string pacakage_description { get; set; }
    }
}