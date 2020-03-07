using ADSM.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ADSM.Models.GuestUser
{
    public class RecommendedActivities : IActivityTrend
    {
        public List<Activities> GetActivityTrend(string region = null)
        {
            throw new NotImplementedException();
        }

        public List<User_Details> GetUserDetails()
        {
            ADSMDbContext dbcontext = new ADSMDbContext();
            var result = dbcontext.Users.Select(x => x).ToList();
            var activities_result = dbcontext.Activities.Select(x => x);
            var ratings_result = dbcontext.ActivityRatings.Select(x => x);

            //var userdetailNames = result.Where(x => x.DOB.Month == 8).Select(x => x.First_Name + " " + x.Last_Name);

            #region Most recommended activties
            // List of all activities 
            var activityRatingList = activities_result.Join(ratings_result, x => x.activity_id, y => y.activity_id,
                (x, y) => new { x.activity_id,x.activity_name,x.activity_fee, y.activity_rating });


            // groupactivities basis ratings
            var groupedactivities = activityRatingList.Select(x=> new {x.activity_id,x.activity_rating }).GroupBy(x => x.activity_id);
            //var activitycount = groupedactivities.Count(x => x.activity_id);

            // calculate average rating
            //var averagerating =; 

            // obtain top three/five results

            // keep others on stand by (should pop up on click of see more in respective order)
            #endregion

            #region Recently bought

            // list of all activities order by purchase date

            // group activities

            // fetch top 3/5

            #endregion

            #region Seasonal trends
            // list of activities 

            // group basis purchase date

            // create enum/switch case to identify seasonal purchases , june - sept - summer; jan-march - winter

            // identify seasons for atcivity 
            
            // group and identify top 3/5

            #endregion

            return result;
        }


    }
}