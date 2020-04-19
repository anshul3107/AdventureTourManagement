using AdventureTourManagement.Interface;
using AdventureTourManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventureTourManagement.Models.GuestUser
{

    public class SeasonalActivities : IActivityService
    {
        ATMDbContext dbContext;
        public SeasonalActivities(ATMDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        List<VMActivityDetails> IActivityService.GetActivity(string region)
        {
            var response = new List<VMActivityDetails>();
            try
            {
                var booked_activities = dbContext.Bookings.Select(x => x).ToList();
                var activities = dbContext.Activities.Select(x => x).ToList();

                var activitiesBooked = booked_activities.Join(activities, x => x.activity_id, y => y.activity_id, (x, y) => new { activities = y, booking = x }).ToList();

                var seasonalActivity = activitiesBooked.Select(x => new SeasonalActivity { activities = x.activities, booking_id = x.booking.booking_id, season = GetSeason(x.booking.booking_date) }).ToList();

                var currentSeason = GetSeason(DateTime.Now);

                seasonalActivity = seasonalActivity.Where(x => x.season == currentSeason).ToList();

                var seasonalActivitiesgrouped = (from a in seasonalActivity
                                                 group a by new { activityID = a.activities.activity_id, season = a.season } into x
                                                 select new { activityId = x.Key.activityID, activityCount = x.Count(), activitySeason = x.Key.season }).AsEnumerable();

                var activitySeason = seasonalActivitiesgrouped.Join(seasonalActivity, x => x.activityId, y => y.activities.activity_id, (x, y) => new { y.activities.activity_name, y.activities.activity_id, y.season, x.activityCount }).ToList();

                var topSeasonalActivity = activitySeason.OrderByDescending(x => x.activityCount).Take(3).ToList();

                List<VMActivityDetails> aresult = new List<VMActivityDetails>();
                foreach (var item in topSeasonalActivity)
                {
                    VMActivityDetails activityItem = new VMActivityDetails();
                    activityItem.activity_id = item.activity_id;
                    activityItem.activity_name = item.activity_name;
                    //activityItem.ActivityAvgRating = item.avgrating;
                    //activityItem.ActivityFee = item.activity_fee;
                    activityItem.activity_season = item.season;
                    aresult.Add(activityItem);
                }

                response = aresult;
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