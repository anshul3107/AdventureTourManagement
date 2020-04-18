using System;
using System.ComponentModel.DataAnnotations;

namespace AdventureTourManagement.Models
{
    public class Regions
    {
        [Key]
        public int region_id { get; set; }
        public string region_name { get; set; }
        public DateTime last_updated_on { get; set; }
    }
}