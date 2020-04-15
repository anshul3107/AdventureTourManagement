
using ADSM.Models;
using ADSM.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            var listofActivities = _service.GetActivities();//send region id
            VMActivity vmactivity = new VMActivity();
            vmactivity.Activities = listofActivities;
            return this.View(vmactivity);
        }

        public ActionResult FetchActivity(int activity_id)
        {
            var activity = _service.GetActivityDetailByID(activity_id);

            VMListActivities fetchActivity = new VMListActivities();
            fetchActivity.ShowActivityList(activity);
            return this.View(fetchActivity);
        }

        public ActionResult FetchAllActivity()
        {
            var allActivities = _service.GetAllActivities();

            VMListActivities fetchAllActivities = new VMListActivities();
            fetchAllActivities.ShowAllActivityList(allActivities);
            return this.View(fetchAllActivities);
        }

        public ActionResult GetUserDetails(int activity_id)
        {
            return RedirectToAction("GetUserDetails", "Shop", new { activity_id = activity_id });
        }
        //activity page --> details buy now
        //Registered user rating option on homepage
        //ask for email and show message in case of an un-registered user.
    }
}