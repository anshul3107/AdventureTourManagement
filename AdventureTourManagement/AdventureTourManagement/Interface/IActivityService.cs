using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureTourManagement.Interface
{
    public interface IActivityService
    {
        List<VMActivityDetails> GetActivity(string region = null);
    }
}
