using System;
using System.ComponentModel.DataAnnotations;

namespace ADSM
{
    public class Bookings
    {
        [Key]
        public int booking_id { get; set; }
        public int activity_id { get; set; }
        public int package_id { get; set; }
        public int user_id { get; set; }
        public DateTime booking_date { get; set; }
    }
}