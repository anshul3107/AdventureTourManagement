using AdventureTourManagement.Models;
using AdventureTourManagement.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventureTourManagement.Interface
{
    public interface IActivityAction
    {
        Task<IDictionary<string, List<VMActivityDetails>>> GetActivities(int region_id = 0);
        Activities GetActivityDetailByID(int activity_id);
        Task<List<Activities>> GetAllActivities(int regionId = 0);
        Task<List<SelectListItem>> GetRegions();
    }
}
