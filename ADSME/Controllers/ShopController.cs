using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADSM.Controllers
{
    public class ShopController : Controller
    {
        public ActionResult Checkout()
        {
            // page for details with submit button
            // order confirmation 

            return this.View();

        }

        public void ShoppingCart()
        {
            //shopping cart library
        }


    }
}