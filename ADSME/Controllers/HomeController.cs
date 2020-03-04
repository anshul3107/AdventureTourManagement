using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.EntityClient;
using ADSM.Models;

namespace ADSM.Controllers
{
    public class HomeController : Controller
    {
        private ADSMDbContext db = new ADSMDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(User_Details user)
        {
            if (ModelState.IsValid)
            {
                if(user.Reg_Id == 0 && user != null)
                {
                    //user.LastUpdatedBy = Session["Name"].ToString();
                    user.LastUpdatedBy = "MJ";
                    user.LastUpdatedOn = DateTime.Now;

                    db.Users.Add(user);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Users()
        {
            List<User_Details> user = db.Users.OrderBy(e => e.Reg_Id).ToList();
            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User_Details user)
        {
            var validUser = db.Users.Where(e => e.Email.Equals(user.Email) && e.Password.Equals(user.Password)).FirstOrDefault();

            if (validUser != null)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Message = "UserName and Password do not match.";
                return View();
            }
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult Master()
        {
            return View();
        }

      

    }
}