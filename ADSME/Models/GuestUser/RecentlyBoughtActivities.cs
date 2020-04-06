using ADSM.Interface;
using ADSM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADSM.Models.GuestUser
{
    public class RecentlyBoughtActivities : IActivityService
    {
       public List<ShowActivity> GetActivity(string region)
        {
            var response = new List<ShowActivity>();
            try
            {
                ADSMDbContext dbContext = new ADSMDbContext();
                var bookings_result = dbContext.Bookings.Select(x => x).ToList();
                var activities_result = dbContext.Activities.Select(x => x).ToList();

                //    list of all activities order by purchase date

                var result = bookings_result.Select(x => new { x.booking_date, x.activity_id});
                var recent_activities = result.OrderByDescending(x => x.booking_date).Select(x=> new { x.booking_date,x.activity_id }).Take(3);

                var activityNames = activities_result.Join(recent_activities, x => new { ActivityID = x.activity_id },
                    y => new { ActivityID = y.activity_id }, (x, y) => new { x.activity_name,x.activity_id });

                List<ShowActivity> aresult = new List<ShowActivity>();
                foreach (var item in activityNames)
                {
                    ShowActivity activityItem = new ShowActivity();
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