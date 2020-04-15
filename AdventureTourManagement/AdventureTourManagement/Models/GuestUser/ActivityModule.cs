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
        private Func<string, IActivityService> _serviceProvider;
        ATMDbContext dbContext;
        //ATMDbContext dbContext = new ATMDbContext();

        public ActivityModule(Func<string, IActivityService> serviceResolver, ATMDbContext dbContext)
        {
            _serviceProvider = serviceResolver;
            this.dbContext = dbContext;
        }

        public IDictionary<string, List<VMActivityDetails>> GetActivities(int region_id = 0)
        {

            IDictionary<string, List<VMActivityDetails>> data = new Dictionary<string, List<VMActivityDetails>>();

            try
            {
                var _serviceSeasonal = _serviceProvider("SA");
                var saActivity = _serviceSeasonal.GetActivity();
                if (saActivity != null)
                    data.Add("SA", saActivity);

                var _serviceRecommended = _serviceProvider("RA");
                var raActivity = _serviceRecommended.GetActivity();
                if (raActivity != null)
                    data.Add("RA", raActivity);

                var _serviceRecentlyBought = _serviceProvider("RB");
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
            var all_activities_result = dbContext.Activities.Select(x => x).ToList();

            return all_activities_result;
        }

        public ActivityRatings RateActivity(int activity_id, string username, int activity_rating)
        {
            throw new NotImplementedException();
        }
    }
    
}