using AdventureTourManagement.Interface;
using AdventureTourManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AdventureTourManagement.Controllers
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
            HttpContext.Session.SetString("CurrentUser", Guid.NewGuid().ToString());
            HttpContext.Session.CommitAsync().Wait();

            var listofActivities = _service.GetActivities(); //send region id
            VMActivity vmactivity = new VMActivity();
            vmactivity.Activities = listofActivities;
            return this.View(vmactivity);
        }

        public ActionResult FetchActivity(int activity_id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")))
            {
                HttpContext.Session.SetString("CurrentUser", Guid.NewGuid().ToString());
            }

            var activity = _service.GetActivityDetailByID(activity_id);

            VMListActivities fetchActivity = new VMListActivities();
            fetchActivity.ShowActivityList(activity);
            return this.View(fetchActivity);
        }

        public ActionResult FetchAllActivity()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")))
            {
                HttpContext.Session.SetString("CurrentUser", Guid.NewGuid().ToString());
            }

            var allActivities = _service.GetAllActivities();

            VMListActivities fetchAllActivities = new VMListActivities();
            fetchAllActivities.ShowAllActivityList(allActivities);
            return this.View(fetchAllActivities);
        }
    }
}