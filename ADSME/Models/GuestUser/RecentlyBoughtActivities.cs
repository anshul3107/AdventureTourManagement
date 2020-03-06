using ADSM.Interface;
using ADSM.Models.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADSM.Models.GuestUser
{
    public class RecentlyBoughtActivities : IActivityTrend
    {
        public List<Activities> GetActivityTrend(string region = null)
        {
            throw new NotImplementedException();
        }
    }
}