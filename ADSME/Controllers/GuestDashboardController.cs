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
        private IActivity _service;

        public GuestDashboardController(IActivity service)
        {
            this._service = service;

        }

        // GET: GuestDashboard
        public ActionResult Index()
        {
            var listofActivities  = _service.GetActivities();
            VMActivity vmactivity = new VMActivity();
            vmactivity.Activities = listofActivities;
            return this.View(vmactivity);
        }

        public ActionResult FetchActivity(int activity_id)
        {
            // further processing
            return this.View();
        }


    }
}