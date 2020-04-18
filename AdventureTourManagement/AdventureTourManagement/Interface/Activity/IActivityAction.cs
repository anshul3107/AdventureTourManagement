using AdventureTourManagement.Models;
using AdventureTourManagement.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventureTourManagement.Interface
{
    public interface IActivityAction
    {
        IDictionary<string, List<VMActivityDetails>> GetActivities(int region_id = 0);
        Activities GetActivityDetailByID(int activity_id);
        List<Activities> GetAllActivities();
        List<Activities> BuyActivity(int activity_id);
        ActivityRatings RateActivity(int activity_id, string username, int activity_rating);
        string ConfirmOrder(int activity_id, string username);
    }
}
