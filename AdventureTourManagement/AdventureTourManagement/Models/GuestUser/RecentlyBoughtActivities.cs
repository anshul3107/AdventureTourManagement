using AdventureTourManagement.Interface;
using AdventureTourManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventureTourManagement.Models.GuestUser
{
    public class RecentlyBoughtActivities : IActivityService
    {
        ATMDbContext dbContext;
        public RecentlyBoughtActivities(ATMDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<VMActivityDetails> GetActivity(string region)
        {
            var response = new List<VMActivityDetails>();
            try
            {
                var bookings_result = dbContext.Bookings.Select(x => x).ToList();
                var activities_result = dbContext.Activities.Select(x => x).ToList();

                //    list of all purchased activities order by purchase date

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