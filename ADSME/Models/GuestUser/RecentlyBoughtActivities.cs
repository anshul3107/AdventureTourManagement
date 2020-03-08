using ADSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADSM.Models.GuestUser
{
    public class RecentlyBoughtActivities : IActivityTrend
    {

        List<dynamic> IActivityTrend.GetActivityTrend(string region)
        {
            var response = new List<dynamic>();
            try
            {
                ADSMDbContext dbContext = new ADSMDbContext();
                var bookings_result = dbContext.Bookings.Select(x => x);
                var activities_result = dbContext.Activities.Select(x => x);

                //    #region Recently bought

                //    list of all activities order by purchase date

                var result = bookings_result.Select(x => x.booking_date);
                var recent_activities = bookings_result.OrderByDescending(x => x.booking_date).Take(3);
                /*                var activityNames = from activity in activities_result
                                                    join recent in recent_activities on activity.activity_id equals recent.activity_id
                                                    select new
                                                    {
                                                        activity.activity_name
                                                    };*/
                var activityNames = activities_result.Join(recent_activities, x => x.activity_id, y => y.activity_id, (x, y) => new { x.activity_name });

                response = activityNames.ToList<dynamic>();
            }
            catch (Exception ex)
            {
                //throw new NotImplementedException();
                throw ex;
            }
            return response;
        }


        //public List<User_Details> GetUserDetails()
        //{
        //    //ADSMDbContext dbcontext = new ADSMDbContext();
        //    //var result = dbcontext.Users.Select(x => x).ToList();
        //    //var activities_result = dbcontext.Activities.Select(x => x);
        //    //var ratings_result = dbcontext.ActivityRatings.Select(x => x);

        //    ////var userdetailNames = result.Where(x => x.DOB.Month == 8).Select(x => x.First_Name + " " + x.Last_Name);

        //    //#region Most recommended activties
        //    //// List of all activities 
        //    //var activityRatingList = activities_result.Join(ratings_result, x => x.activity_id, y => y.activity_id,
        //    //    (x, y) => new { x.activity_id,x.activity_name,x.activity_fee, y.activity_rating });


        //    //// groupactivities basis ratings
        //    //var groupedactivities = activityRatingList.Select(x=> new { x.activity_id, avgrating = activityRatingList.Where(y => y.activity_id == x.activity_id).Average(y => y.activity_rating) });

        //    //// obtain top three/five results
        //    //var topActivities = groupedactivities.OrderByDescending(x => x.avgrating).Take(3);

        //    //var otherActivities = groupedactivities.Except(topActivities);

        //    //var recommActivityResult = topActivities.Join(activityRatingList, x => x.activity_id, y => y.activity_id, (x, y) => new { x.activity_id, x.avgrating, y.activity_name, y.activity_fee });

        //    //return recommActivityResult;
        //    //// keep others on stand by (should pop up on click of see more in respective order)
        //    //#endregion

        //    #region Recently bought

        //    // list of all activities order by purchase date

        //    // group activities

        //    // fetch top 3/5

        //    #endregion

        //    #region Seasonal trends
        //    // list of activities 

        //    // group basis purchase date

        //    // create enum/switch case to identify seasonal purchases , june - sept - summer; jan-march - winter

        //    // identify seasons for atcivity 

        //    // group and identify top 3/5

        //    #endregion

        //   // return result;
        //}

    }
}