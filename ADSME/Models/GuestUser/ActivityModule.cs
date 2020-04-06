using ADSM.Interface;
using ADSM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADSM.Models.GuestUser
{
    public class ActivityModule : IActivityAction
    {
        private IActivityService _serviceRecentlyBought;
        private IActivityService _serviceRecommended;
        private IActivityService _serviceSeasonal;

        ADSMDbContext dbContext = new ADSMDbContext();


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

                var raActivity = _serviceRecommended.GetActivity();
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

        public List<Activities> BuyActivity(int activity_id)
        {
            throw new NotImplementedException();
        }

        public string ConfirmOrder(int activity_id, string username)
        {
            throw new NotImplementedException();
        }

        public Activities GetActivityDetailByID(int activity_id)
        {
            var activity_details_result = dbContext.Activities.Select(x => x).Where(x => x.activity_id == activity_id).FirstOrDefault();

            return activity_details_result;
        }

        public List<Activities> GetAllActivities()
        {
            var all_activities = dbContext.Activities.Select(x => x).ToList();

            return all_activities;
        }

        public ActivityRatings RateActivity(int activity_id, string username, int activity_rating)
        {
            throw new NotImplementedException();
        }
    }
    
}