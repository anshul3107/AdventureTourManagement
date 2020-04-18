using System.ComponentModel.DataAnnotations;

namespace AdventureTourManagement.Models
{
    public class Packages
    {
        [Key]
        public int package_id { get; set; }
        public string pacakage_description { get; set; }
    }
}