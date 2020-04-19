using AdventureTourManagement.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventureTourManagement.Interface
{
    public interface IActivityService
    {
        Task<List<VMActivityDetails>> GetActivity(int region_id = 0);
    }
}
