using ADSM.Interface;
using ADSM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADSM.Models.GuestUser
{
    public class ActivityModule : IActivity
    {
        private IActivityService _serviceRecentlyBought;
        private IActivityService _serviceRecommended;
        private IActivityService _serviceSeasonal;

        public ActivityModule(IActivityService serviceSA,IActivityService serviceRB, IActivityService serviceRA) //Func<string, IActivityService> serviceResolver))
        {
            this._serviceSeasonal = serviceSA;
            this._serviceRecentlyBought = serviceRB;
            this._serviceRecommended = serviceRA;
        }
        public IDictionary<string, List<ShowActivity>> GetActivities(int region_id = 0)
        {

            IDictionary<string, List<ShowActivity>> data = new Dictionary<string, List<ShowActivity>>();

            try
            {
                var saActivity = _serviceSeasonal.GetActivity();
                if (saActivity != null)
                    data.Add("SA", saActivity);

                var raActivity = _serviceSeasonal.GetActivity();
                if (raActivity != null)
                    data.Add("RA", raActivity);

                var rbActivity = _serviceRecentlyBought.GetActivity();
                if (rbActivity != null)
                    data.Add("RB", rbActivity);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return data;
        }
    }
    
}