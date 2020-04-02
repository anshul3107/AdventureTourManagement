using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADSM.Models
{
    public class Bookings
    {
        [Key]
        public int booking_id { get; set; }
        public int activity_id { get; set; }
        public int? package_id { get; set; }
        public int? user_id { get; set; }
        public DateTime booking_date { get; set; }
    }
}