using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureTourManagement.Models
{
    public class ActivityRatings
    {
        [Key]
        public int id { get; set; }
        public int activity_id { get; set; }
        public int activity_rating { get; set; }
        [ForeignKey("activity_id")]
        public Activities activities { get; set; }
    }
}