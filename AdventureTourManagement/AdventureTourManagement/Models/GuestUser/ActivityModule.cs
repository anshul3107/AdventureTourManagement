using AdventureTourManagement.Interface;
using AdventureTourManagement.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureTourManagement.Models.GuestUser
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

        public async Task<IDictionary<string, List<VMActivityDetails>>> GetActivities(int region_id = 0)
        {
            IDictionary<string, List<VMActivityDetails>> data = new Dictionary<string, List<VMActivityDetails>>();

            try
            {
                var _serviceSeasonal = _serviceProvider("SA");
                var saActivity =await _serviceSeasonal.GetActivity(region_id);
                if (saActivity != null)
                    data.Add("SA", saActivity);

                var _serviceRecommended = _serviceProvider("RA");
                var raActivity = await _serviceRecommended.GetActivity(region_id);
                if (raActivity != null)
                    data.Add("RA", raActivity);

                var _serviceRecentlyBought = _serviceProvider("RB");
                var rbActivity = await _serviceRecentlyBought.GetActivity(region_id);
                if (rbActivity != null)
                    data.Add("RB", rbActivity);



            }
            catch (Exception ex)
            {
                throw ex;
            }

            return data;
        }


        public Activities GetActivityDetailByID(int activity_id)
        {
                var activity_details_result = dbContext.Activities.Select(x => x).Where(x => x.activity_id == activity_id).FirstOrDefault();
                
                return activity_details_result;
         
        }

        public async Task<List<Activities>> GetAllActivities(int regionId = 0)
        {
            if (regionId > 0)
            {
                var activityRegionMap = await dbContext.ActivityRegionMapping.Where(x => x.region_id == regionId).ToListAsync();
                var all_activities_result = dbContext.Activities.Join(activityRegionMap,x=>x.activity_id,y=>y.activity_id,(x,y)=>x).ToList();

                return all_activities_result;
            }
            else
            {
                var all_activities_result = dbContext.Activities.Select(x => x).ToList();

                return all_activities_result;
            }
        }

       public async Task<List<SelectListItem>> GetRegions()
        {
            var regions = await dbContext.Regions.Select(x => new SelectListItem
            {
                Text = x.region_name,
                Value = x.region_id.ToString()
            }).ToListAsync();
            return regions;
        }
    }

}