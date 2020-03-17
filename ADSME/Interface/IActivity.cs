using ADSM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADSM.Interface
{
   public interface IActivity
    {
        IDictionary<string, List<dynamic>> GetActivities(int region_id = 0);
    }
}
