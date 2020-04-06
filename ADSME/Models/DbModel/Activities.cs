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
        public int activity_id { get; set; }
        public string activity_name { get; set; }
        public int activity_slots { get; set; }
        public int activity_fee { get; set; }
        public string activity_description { get; set; }
        public DateTime last_updated_on { get; set; }
    }
}