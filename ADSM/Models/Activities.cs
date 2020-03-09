using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADSM.Models
{
    public class Activities
    {
        [Key]
        public int Activity_id { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public double? Fee { get; set; }
        public bool Summer { get; set; }
        public bool Winter { get; set; }
        public bool Autumn { get; set; }
        public bool Spring { get; set; }
        public bool Equipment_included { get; set; }
        public double? Equipment_Cost { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
 
    }
}