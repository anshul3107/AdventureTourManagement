using ADSM.Interface;
using ADSM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADSM.Controllers
{
    public class GuestDashboardController : Controller
    {
        private IActivity _service;

        public GuestDashboardController(IActivity service)
        {
            this._service = service;

        }

        // GET: GuestDashboard
        public ActionResult Index()
        {
            var listofActivities  = _service.GetActivities();
            return this.View();
        }

        public ActionResult FetchActivity(int activity_id)
        {
            // further processing
            return this.View();
        }


    }
}