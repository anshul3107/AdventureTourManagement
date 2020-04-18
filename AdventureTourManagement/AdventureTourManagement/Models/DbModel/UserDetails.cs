using System;
using System.ComponentModel.DataAnnotations;

namespace AdventureTourManagement.Models
{
    public class UserDetails
    {
        [Key]
        public int Reg_id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public DateTime DOB { get; set; }
        public string emial { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string Confirm_Password { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}