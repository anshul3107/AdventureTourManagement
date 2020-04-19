using System;
using System.ComponentModel.DataAnnotations;

namespace AdventureTourManagement.Models
{
    public class Bookings
    {
        [Key]
        public int booking_id { get; set; }
        public int activity_id { get; set; }
        public string user_name { get; set; }
        public DateTime booking_date { get; set; }
    }
}