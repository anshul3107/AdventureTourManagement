using AdventureTourManagement.Interface;
using AdventureTourManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult> Index(int regionId =0)
        {
            HttpContext.Session.SetString("CurrentUser", Guid.NewGuid().ToString());
            HttpContext.Session.CommitAsync().Wait();
            var lstRegions =await _service.GetRegions();
            lstRegions.Add(new SelectListItem() { Text = "All", Value = "0" });
            var listofActivities = await _service.GetActivities(regionId); //send region id
            VMActivity vmactivity = new VMActivity();
            vmactivity.Activities = listofActivities;
            vmactivity.Regions = lstRegions.OrderBy(x=>x.Value).ToList();
            vmactivity.RegionSelected = regionId.ToString();
            return this.View(vmactivity);
        }

        public async Task<IActionResult> FilterActivities(VMActivity activityFilter)
        {
            if(activityFilter != null)
            {
                if (!string.IsNullOrEmpty(activityFilter.RegionSelected))
                {
                    if (activityFilter.RegionSelected == "0")
                    {
                        return RedirectToAction("Index", new { regionId = 0 });
                    }
                    else
                    {
                        int region_id = Convert.ToInt32(activityFilter.RegionSelected);
                        return RedirectToAction("Index", new { regionId = region_id });
                    }
                }
            }

            return RedirectToAction("Index", new { regionId = 0 });
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

        public async Task<ActionResult> FetchAllActivity()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")))
            {
                HttpContext.Session.SetString("CurrentUser", Guid.NewGuid().ToString());
            }

            var allActivities =await _service.GetAllActivities();

            VMListActivities fetchAllActivities = new VMListActivities();
            fetchAllActivities.ShowAllActivityList(allActivities);
            return this.View(fetchAllActivities);
        }
    }
}