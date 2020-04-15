using ADSM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
//using SecureAccess;
//using SecureAccess.Interface;
//using SecureAccess.Model;

namespace ADSM.Controllers
{
    public class ShopController : Controller
    {
        public ActionResult Checkout()
        {
            // validate email
            //page for details with submit button
            // order confirmation 

            return this.View();
        }

        Task<Guid> tokenId;

        public ActionResult GetUserDetails(int activity_id)
        {
            VMUserDetail user_details = new VMUserDetail();
            user_details.activity_id = activity_id;

            return this.View(user_details);
        }
        public ActionResult AuthenticateUserEmail(string email)
        {
            Authentication authentication = new Authentication();

            AuthenticationInput authInputs = new AuthenticationInput();
            authInputs.AuthenticationType = Constants.AuthenticationType.Email;
            authInputs.AuthenticationMode = Constants.AuthneticationMode.TokenBasedAuthention;
            authInputs.Receiver = email;
            authInputs.Subject = "Adventure Tour Management Token Verification";

            tokenId = authentication.Authenticate(authInputs);

            return this.View();
        }

        //public ActionResult verifyToken(string userOTP)
        //{
        //    VerificationInput verifInputs = new VerificationInput();
        //verifInputs.TransactionIdentifier = tokenId;
        //    verifInputs.TransactionToken = userOTP;

        //authentication.Verify(tokenId);


        //    return this.View();
        //}

        public void ShoppingCart()
        {
            //shopping cart library
        }

        public void AddToCart()
        {

        }

    }
}