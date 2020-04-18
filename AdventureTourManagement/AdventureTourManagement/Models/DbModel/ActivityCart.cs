using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureTourManagement.Models
{
    public class ActivityCart
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string ItemDetails { get; set; }
    }
}
