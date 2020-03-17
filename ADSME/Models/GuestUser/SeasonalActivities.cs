using ADSM.Interface;
using ADSM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADSM.Models.GuestUser
{
    public class SeasonalActivities : IActivityService
    {
        List<dynamic> IActivityService.GetActivity(string region)
        {
            var response = new List<dynamic>();
            try
            {
                ADSMDbContext dbContext = new ADSMDbContext();
                var booked_activities = dbContext.Bookings.Select(x => x);
                var activities = dbContext.Activities.Select(x => x);

                //    #region Seasonal trends
                // list of activities
                // var winter_activities = booked_activities.Where(x => (x.booking_date.Month <2 && x.booking_date.Month ));

                // list of bookings 

                var activitiesBooked = booked_activities.Join(activities, x => x.activity_id, y => y.activity_id, (x, y) => new { activities = y, booking = x });

                var seasonalActivity = activitiesBooked.Select(x => new SeasonalActivity { activities = x.activities, booking_id = x.booking.booking_id, season = GetSeason(x.booking.booking_date) });

                var seasonalActivitiesgrouped = from a in seasonalActivity
                                                group a by new { activityID = a.activities.activity_id, season = a.season } into x
                                                select new { activityId = x.Key.activityID, activityCount = x.Count(), activitySeason = x.Key.season };

                var activitySeason = seasonalActivitiesgrouped.Join(seasonalActivity, x => x.activityId, y => y.activities.activity_id, (x, y) => new { y.activities.activity_name, y.season, x.activityCount });

                var topSeasonalActivity = activitySeason.OrderByDescending(x => x.activityCount).Take(3);

                response = topSeasonalActivity.ToList<dynamic>();

                // create a DTO booking -> activity id  , season 

                //booking id , activity id, date


                // group basis purchase date

                // create enum/switch case to identify seasonal purchases, june - aug = Summer; dec-feb = winter; mar-may = Spring; Sep-nov = Autumn

                // identify seasons for atcivity

                // group and identify top 3/5

                //     #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        private string GetSeason(DateTime bookingDate)
        {
            string season = string.Empty;
            try
            {
                switch (bookingDate.Month)
                {
                    case 0:
                        season = "Winter";
                        break;
                    case 1:
                        season = "Winter";
                        break;
                    case 2:
                        season = "Spring";
                        break;
                    case 3:
                        season = "Spring";
                        break;
                    case 4:
                        season = "Spring";
                        break;
                    case 5:
                        season = "Summer";
                        break;
                    case 6:
                        season = "Summer";
                        break;
                    case 7:
                        season = "Summer";
                        break;
                    case 8:
                        season = "Autumn";
                        break;
                    case 9:
                        season = "Autumn";
                        break;
                    case 10:
                        season = "Autumn";
                        break;
                    case 11:
                        season = "Winter";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return season;
        }
    }

    internal class SeasonalActivity
    {
        public Activities activities { get; set; }
        public int booking_id { get; set; }
        public string season { get; set; }
    }

}