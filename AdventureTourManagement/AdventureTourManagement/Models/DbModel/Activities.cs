using System;
using System.ComponentModel.DataAnnotations;

namespace AdventureTourManagement.Models
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
        public string activity_image_path { get; set; }
    }
}