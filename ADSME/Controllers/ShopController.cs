using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecureAccess;
using SecureAccess.Interface;
using SecureAccess.Model;

namespace ADSM.Controllers
{
    public class ShopController : Controller
    {
        public ActionResult Checkout(string email)
        {
            // validate email
            //page for details with submit button
            // order confirmation 
            Authentication authentication = new Authentication();

            AuthenticationInput authInputs = new AuthenticationInput();
            authInputs.AuthenticationType = Constants.AuthenticationType.Email;
            authInputs.AuthenticationMode = Constants.AuthneticationMode.TokenBasedAuthention;
            authInputs.Receiver = email;
            authInputs.Subject = "Adventure Tour Management Token Verification";

            //authentication.Authenticate(authInputs);



            return this.View();

        }

        public void ShoppingCart()
        {
            //shopping cart library
        }

        public void AddToCard()
        {

        }

    }
}