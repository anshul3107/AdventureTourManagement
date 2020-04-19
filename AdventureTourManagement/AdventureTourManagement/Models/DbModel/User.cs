using System;
using System.ComponentModel.DataAnnotations;

namespace AdventureTourManagement.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string user_name { get; set; }
        public string user_email { get; set; }
        public string user_contact { get; set; }
        public string user_encryptedkey { get; set; }
        public DateTime? modified_date { get; set; }
        public string user_encryptedmessage { get; set; }
    }
}