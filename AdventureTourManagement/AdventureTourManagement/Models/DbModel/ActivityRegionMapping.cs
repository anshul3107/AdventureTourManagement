using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureTourManagement.Models.DbModel
{
    public class ActivityRegionMapping
    {
        [Key]
        public int id { get; set; }
        public int activity_id { get; set; }
        public int region_id { get; set; }
    }
}
