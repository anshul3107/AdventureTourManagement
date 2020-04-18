using AdventureTourManagement.ViewModels;
using System.Collections.Generic;

namespace AdventureTourManagement.Interface
{
    public interface IActivityService
    {
        List<VMActivityDetails> GetActivity(string region = null);
    }
}
