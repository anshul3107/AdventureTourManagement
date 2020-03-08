using ADSM.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace ADSM.Models.GuestUser
{
    public class RecommendedActivities : IActivityTrend
    {
        public List<dynamic> GetActivityTrend(string region = null)
        {
            var response = new List<dynamic>();
            try
            {
                ADSMDbContext dbcontext = new ADSMDbContext();
                var activities_result = dbcontext.Activities.Select(x => x);
                var ratings_result = dbcontext.ActivityRatings.Select(x => x);


                #region Most recommended activties
                // List of all activities 
                var activityRatingList = activities_result.Join(ratings_result, x => x.activity_id, y => y.activity_id,
                    (x, y) => new { x.activity_id, x.activity_name, x.activity_fee, y.activity_rating });


                // groupactivities basis ratings
                var groupedactivities = activityRatingList.Select(x => new { x.activity_id, avgrating = activityRatingList.Where(y => y.activity_id == x.activity_id).Average(y => y.activity_rating) });

                // obtain top three/five results
                var topActivities = groupedactivities.OrderByDescending(x => x.avgrating).Take(3);

                var otherActivities = groupedactivities.Except(topActivities);

                var recommActivityResult = topActivities.Join(activityRatingList, x => x.activity_id, y => y.activity_id, (x, y) => new { x.activity_id, x.avgrating, y.activity_name, y.activity_fee }).ToList();

                List<dynamic> result = new List<dynamic>();
                foreach (var item in recommActivityResult)
                {
                    dynamic activityItem = new ExpandoObject();
                    activityItem.ActivityID = item.activity_id;
                    activityItem.ActivityName = item.activity_name;
                    activityItem.ActivityAvgRating = item.avgrating;
                    activityItem.ActivityFee = item.activity_fee;
                    result.Add(item);
                }
                response = result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
            // keep others on stand by (should pop up on click of see more in respective order)
            #endregion
        }
    }
}