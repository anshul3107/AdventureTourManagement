using AdventureTourManagement.Models;
using System;
using System.Collections.Generic;

namespace AdventureTourManagement.ViewModels
{
    public class VMActivity
    {
        public IDictionary<string,List<VMActivityDetails>> Activities { get; set; }
    }

    public class VMActivityDetails :IDisposable
    {
        public int activity_id { get; set; }
        public string activity_name { get; set; }
        public int activity_slots { get; set; }
        public int activity_fee { get; set; }
        public string activity_description { get; set; }
        public DateTime last_updated_on { get; set; }
        public string activity_image_path { get; set; }
        public string activity_season { get; set; }

        public void Dispose()
        {
            activity_id = 0;
            activity_name = null;
            activity_slots = 0;
            activity_fee = 0;
            activity_description = null;
            activity_image_path = null;
        }
    }

    public class VMListActivities
    {
        public List<VMActivityDetails> showActivities { get; set; }

        public void ShowActivityList(Activities activity)
        {
            List<VMActivityDetails> result = new List<VMActivityDetails>();
            //using (VMActivityDetails activityItem = new VMActivityDetails())
            //{

            VMActivityDetails activityItem = new VMActivityDetails();
            activityItem.activity_id = activity.activity_id;
            activityItem.activity_name = activity.activity_name;
            activityItem.activity_fee = activity.activity_fee;
            activityItem.activity_slots = activity.activity_slots;
            activityItem.activity_description = activity.activity_description;
            activityItem.activity_image_path = activity.activity_image_path;

            result.Add(activityItem);
            //}

            this.showActivities = result;
        }

        public void ShowAllActivityList(List<Activities> activity)
        {
            List<VMActivityDetails> result = new List<VMActivityDetails>();

            foreach (var act in activity)
            {
                //using (VMActivityDetails activityDetails = new VMActivityDetails())
                //{
                VMActivityDetails activityDetails = new VMActivityDetails();
                activityDetails.activity_id = act.activity_id;
                activityDetails.activity_name = act.activity_name;
                activityDetails.activity_fee = act.activity_fee;
                activityDetails.activity_slots = act.activity_slots;
                activityDetails.activity_description = act.activity_description;

                result.Add(activityDetails);
                //}
            }
            this.showActivities = result;
        }

    }
}