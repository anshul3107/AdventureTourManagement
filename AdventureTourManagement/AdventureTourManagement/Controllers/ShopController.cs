using AdventureTourManagement.Interface.Shopping;
using AdventureTourManagement.Models;
using AdventureTourManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureAccess;
using SecureAccess.Model;
using System;
using System.Threading.Tasks;

namespace AdventureTourManagement.Controllers
{
    public class ShopController : Controller
    {
        IShopping _shopping;

        public ShopController(IShopping shopping)
        {
            _shopping = shopping;
        }

        // buy Now
        // Place order
        public ActionResult GetUserDetails()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")))
            {

                VMUserDetail user_details = new VMUserDetail();
                user_details.IsToken = false;
                return this.View(user_details);
            }
            else
            {
                return View("error");
            }
        }

        public async Task<ActionResult> BuyNowAsync(int activityId)
        {
            string userEmail = string.Empty;

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")))
            {
                userEmail = HttpContext.Session.GetString("CurrentUser");
            }

            var result = await _shopping.AddToCart(activityId, userEmail);

            return RedirectToAction("GetUserDetails");
        }

        public async Task<ActionResult> AuthenticateUserEmail(VMUserDetail email)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")))
            {

                var tokenId = await _shopping.AuthenticateUser(email.user_email);

                VMUserDetail user_details = new VMUserDetail();
                user_details.user_email = email.user_email;
                user_details.userAuthID = tokenId;
                user_details.IsToken = true;

                return this.View("GetUserDetails", user_details);
            }
            else
                return View("error");
        }

        public async Task<ActionResult> ResendAuthToken(VMUserDetail email)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")))
            {

                var tokenId = await _shopping.AuthenticateUser(email.user_email);

                VMUserDetail user_details = new VMUserDetail();
                user_details.user_email = email.user_email;
                user_details.userAuthID = tokenId;
                user_details.IsToken = true;
                user_details.Message = string.Empty;

                return this.View("GetUserDetails", user_details);
            }
            else
                return View("Error");
        }

        public async Task<ActionResult> VerifyTokenAsync(VMUserDetail email)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")))
            {

                bool verificationresult = await _shopping.VerifyUserToken(email.user_email, email.userAuthID, email.Token);

                if (verificationresult)
                {
                    await _shopping.SendBookingConfirmation(email.user_email);

                    return this.View(verificationresult);
                }
                else
                {
                    var tokenId = await _shopping.AuthenticateUser(email.user_email);

                    VMUserDetail user_details = new VMUserDetail();
                    user_details.user_email = email.user_email;
                    user_details.userAuthID = tokenId;
                    user_details.IsToken = true;

                    return this.View("GetUserDetails", user_details);
                }
            }
            else
                return View("Error");


        }

        // proceed to checkout --> place order
        
        public async Task<ActionResult> ViewShoppingCart()
        {
            string userEmail = string.Empty;

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")))
            {
                userEmail = HttpContext.Session.GetString("CurrentUser");
            }

            var result = await _shopping.FetchShoppingCart(userEmail);
            VMActivityCart vmobj = new VMActivityCart()
            {
                CartItem = result
            };

            return View(vmobj);
        }

        //TODO : create view -> go to cart / continue shopping (home)
        public async Task<ActionResult> AddToCart(int activityId)
        {
            string userEmail = string.Empty;

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")))
            {
                userEmail = HttpContext.Session.GetString("CurrentUser");
            }

            var result = await _shopping.AddToCart(activityId,userEmail);

            var vmObj = ActivityCartDTO.TransformcartItem(result,activityId);
            return View(vmObj);
        }

        // cart
        public async Task<IActionResult> DeleteFromCart(int activityId)
        {
            string userEmail = string.Empty;

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")))
            {
                userEmail = HttpContext.Session.GetString("CurrentUser");
            }

            
            await _shopping.RemoveFromCart(activityId, userEmail);

           return RedirectToAction("ViewShoppingCart");
        }

    }
}