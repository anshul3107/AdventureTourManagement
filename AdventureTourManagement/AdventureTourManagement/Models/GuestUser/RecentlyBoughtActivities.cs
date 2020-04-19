using AdventureTourManagement.Interface;
using AdventureTourManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureTourManagement.Models.GuestUser
{
    public class RecentlyBoughtActivities : IActivityService
    {
        ATMDbContext dbContext;
        public RecentlyBoughtActivities(ATMDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<VMActivityDetails>> GetActivity(int region_id = 0)
        {
            var response = new List<VMActivityDetails>();
            try
            {
                List<Activities> activities_result = new List<Activities>();
                if (region_id > 0)
                {
                    var activityRegionMap = await dbContext.ActivityRegionMapping.Where(x => x.region_id == region_id).ToListAsync();

                     activities_result = dbContext.Activities.Join(activityRegionMap,x=>x.activity_id,y=>y.activity_id,(x,y) => x).ToList();
                }
                else
                {

                     activities_result = dbContext.Activities.Select(x => x).ToList();
                }
                var bookings_result = dbContext.Bookings.Select(x => x).ToList();

                var result = bookings_result.Select(x => new { x.booking_date, x.activity_id });
                var recent_activities = result.OrderByDescending(x => x.booking_date).Select(x => new { x.booking_date, x.activity_id }).Take(3);

                var activityNames = activities_result.Join(recent_activities, x => new { ActivityID = x.activity_id },
                    y => new { ActivityID = y.activity_id }, (x, y) => new { x.activity_name, x.activity_id });

                List<VMActivityDetails> aresult = new List<VMActivityDetails>();
                foreach (var item in activityNames)
                {
                    VMActivityDetails activityItem = new VMActivityDetails();
                    activityItem.activity_id = item.activity_id;
                    activityItem.activity_name = item.activity_name;
                    //activityItem.ActivityAvgRating = item.avgrating;
                    //activityItem.ActivityFee = item.activity_fee;
                    aresult.Add(activityItem);
                }

                response = aresult;
            }
            catch (Exception ex)
            {
                //throw new NotImplementedException();
                throw ex;
            }
            return response;
        }
    }
}