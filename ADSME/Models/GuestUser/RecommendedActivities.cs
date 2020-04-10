using ADSM.Interface;
using ADSM.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace ADSM.Models.GuestUser
{
    public class RecommendedActivities : IActivityService
    {
        public List<VMActivityDetails> GetActivity(string region = null)
        {
            var response = new List<VMActivityDetails>();
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
                var groupedactivities = activityRatingList.Select(x => new { x.activity_id, x.activity_name,x.activity_fee, avgrating = activityRatingList.Where(y => y.activity_id == x.activity_id).Average(y => y.activity_rating) });//).GroupBy(y => y.activity_id);

                // obtain top three/five results
                var recommActivityResult = groupedactivities.Distinct().OrderByDescending(x => x.avgrating).Take(3);

                //var otherActivities = groupedactivities.Except(topActivities);

                //var recommActivityResult = topActivities.Join(groupedactivities, x => x.activity_id, y => y.activity_id, (x, y) => new { x.activity_id, x.avgrating, y.activity_name, y.activity_fee }).ToList();

                List<VMActivityDetails> result = new List<VMActivityDetails>();
                foreach (var item in recommActivityResult)
                {
                    VMActivityDetails activityItem = new VMActivityDetails();
                    activityItem.activity_id = item.activity_id;
                    activityItem.activity_name = item.activity_name;
                    //activityItem.ActivityAvgRating = item.avgrating;
                    //activityItem.ActivityFee = item.activity_fee;
                    result.Add(activityItem);
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