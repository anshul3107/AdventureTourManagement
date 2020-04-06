using ADSM.Interface;
using ADSM.Models;
using ADSM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADSM.Controllers
{
    public class GuestDashboardController : Controller
    {
        private IActivityAction _service;

        public GuestDashboardController(IActivityAction service)
        {
            this._service = service;
        }

        // GET: GuestDashboard
        public ActionResult Index()
        {
            var listofActivities  = _service.GetActivities();//send region id
            VMActivity vmactivity = new VMActivity();
            vmactivity.Activities = listofActivities;
            return this.View(vmactivity);
        }

        public ActionResult FetchActivityDetails(int activity_id)
        {
           var activity_result = _service.GetActivityDetailByID(activity_id);
            ShowActivity fetchActivity = new ShowActivity();
            fetchActivity.activity_description = activity_result.activity_description;
            fetchActivity.activity_fee = activity_result.activity_fee;
            fetchActivity.activity_name = activity_result.activity_name;
            fetchActivity.activity_slots = activity_result.activity_slots;
            fetchActivity.activity_id = activity_result.activity_id;

            return this.View(fetchActivity);
        }

        public ActionResult FetchAllActivity ()
        {
            // fetch relevant all activities, 
            var allActivities = _service.GetAllActivities();
            ShowActivity vmAllActivities = new ShowActivity();

            foreach (var act in allActivities)
            {
                vmAllActivities.activity_description = act.activity_description;
                vmAllActivities.activity_id = act.activity_id;
                vmAllActivities.activity_fee = act.activity_fee;
                vmAllActivities.activity_name = act.activity_name;
                vmAllActivities.activity_slots = act.activity_slots;
            }

            return View(vmAllActivities);
        }

        // 
        //public ActionResult ShowActivityDetails(int activity_id)
        //{
        //    // fetch activity details
        //    var activityDetails = _service.
        //    return View();
        //}

        //activity page --> details buy now
        //Registered user rating option on homepage
        //ask for email and show message in case of an un-registered user.
    }
}