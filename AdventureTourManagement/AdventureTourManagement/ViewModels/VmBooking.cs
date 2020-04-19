using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureTourManagement.ViewModels
{
    public class VmBooking
    {
        public string ActivityName { get; set; }
        public string  ActivityDesc { get; set; }
        public int ActivityFee { get; set; }
        public int    ActivityId { get; set; }
        public string ActivityImage { get; set; }
        public DateTime BookingDate { get; set; }
        public string UserEmail { get; set; }
    }

    public class VmBookinglist
    {
        public List<VmBooking> Bookings { get; set; }
    }
}
