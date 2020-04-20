using AdventureTourManagement.Interface;
using AdventureTourManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureTourManagement.Models.GuestUser
{

    public class SeasonalActivities : IActivityService
    {
        ATMDbContext dbContext;
        public SeasonalActivities(ATMDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<VMActivityDetails>> GetActivity(int region_id = 0)
        {
            var response = new List<VMActivityDetails>();
            try
            {

                List<Activities> activities = new List<Activities>();
                if (region_id > 0)
                {
                    var activityRegionMap = await dbContext.ActivityRegionMapping.Where(x => x.region_id == region_id).ToListAsync();

                    activities = dbContext.Activities.Join(activityRegionMap, x => x.activity_id, y => y.activity_id, (x, y) => x).ToList();
                }
                else
                {

                    activities = dbContext.Activities.Select(x => x).ToList();
                }


                var booked_activities = dbContext.Bookings.Select(x => x).ToList();

                var activitiesBooked = booked_activities.Join(activities, x => x.activity_id, y => y.activity_id, (x, y) => new { activities = y, booking = x }).ToList();

                var seasonalActivity = activitiesBooked.Select(x => new SeasonalActivity { activities = x.activities, booking_id = x.booking.booking_id, season = GetSeason(x.booking.booking_date) }).ToList();

                var currentSeason = GetSeason(DateTime.Now);

                seasonalActivity = seasonalActivity.Where(x => x.season == currentSeason).ToList();

                var seasonalActivitiesgrouped = (from a in seasonalActivity
                                                 group a by new { activityID = a.activities.activity_id, season = a.season } into x
                                                 select new { activityId = x.Key.activityID, activityCount = x.Count(), activitySeason = x.Key.season}).AsEnumerable();

              //  var activitySeason = seasonalActivitiesgrouped.Join(seasonalActivity, x => x.activityId, y => y.activities.activity_id, (x, y) => new { y.activities.activity_name, y.activities.activity_id, y.season, x.activityCount }).ToList();

                var topSeasonalActivity = seasonalActivitiesgrouped.OrderByDescending(x => x.activityCount).Take(3).ToList();

                List<VMActivityDetails> aresult = new List<VMActivityDetails>();
                foreach (var item in topSeasonalActivity)
                {
                    VMActivityDetails activityItem = new VMActivityDetails();
                    activityItem.activity_id = item.activityId;
                    activityItem.activity_name = activities.Where(x=>x.activity_id == item.activityId).FirstOrDefault().activity_name;
                    //activityItem.ActivityAvgRating = item.avgrating;
                    //activityItem.ActivityFee = item.activity_fee;
                    activityItem.activity_season = item.activitySeason;
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