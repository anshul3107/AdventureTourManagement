using System;
using System.ComponentModel.DataAnnotations;

namespace ADSM.Models
{
    public class Activities
    {
        [Key]
        public int activity_id { get; set; }
        public string activity_name { get; set; }
        public int activity_slots { get; set; }
        public int activity_fee { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}