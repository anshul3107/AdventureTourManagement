using AdventureTourManagement.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureTourManagement.ViewModels
{
    public class ActivityCartDTO
    {
        public int ActivityID { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public int Quantity { get; set; }
        public double ActivityFee { get; set; }
        public string ActivityImage { get; set; }

        public static ActivityCartDTO TransformcartItem(ActivityCart cartItem, int activityID)
        {
            var item = JsonConvert.DeserializeObject<List<ActivityCartDTO>>(cartItem.ItemDetails);

            ActivityCartDTO returnItem = item.Where(x => x.ActivityID == activityID).FirstOrDefault();

            return returnItem;

        }
    }



    public class VMActivityCart
    {
        public List<ActivityCartDTO> CartItem { get; set; }
    }
}
