using AdventureTourManagement.Interface;
using AdventureTourManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureTourManagement.Models.GuestUser
{
    public class RecommendedActivities : IActivityService
    {
        ATMDbContext dbcontext;
        public RecommendedActivities(ATMDbContext dbContext)
        {
            this.dbcontext = dbContext;

        }

        public async Task<List<VMActivityDetails>> GetActivity(int region_id = 0)
        {
            var response = new List<VMActivityDetails>();
            try
            {
                List<Activities> activities_result = new List<Activities>();
                if (region_id > 0)
                {
                    var activityRegionMap = await dbcontext.ActivityRegionMapping.Where(x => x.region_id == region_id).ToListAsync();

                    activities_result = dbcontext.Activities.Join(activityRegionMap, x => x.activity_id, y => y.activity_id, (x, y) => x).ToList();
                }
                else
                {

                    activities_result = dbcontext.Activities.Select(x => x).ToList();
                }

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