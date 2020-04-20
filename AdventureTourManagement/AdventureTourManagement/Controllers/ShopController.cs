using AdventureTourManagement.Interface.Shopping;
using AdventureTourManagement.Models;
using AdventureTourManagement.Utility;
using AdventureTourManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecureAccess;
using SecureAccess.Helper;
using SecureAccess.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureTourManagement.Controllers
{
    public class ShopController : Controller
    {
        IShopping _shopping;
        ILogger<ShopController> _logger;
        EncryptionDecryption _encryption;
        
        public ShopController(IShopping shopping, ILogger<ShopController> logger, IServiceProvider provider)
        {
            _shopping = shopping;
            _logger = logger;

            SecureAccessFactory sa = new SecureAccessFactory();
            _encryption = sa.CreateInstance(provider).SecureAccess.GetEncryptionDecryption;
        }

        // buy Now
        // Place order
        public ActionResult GetUserDetails(int isForgetPassword, int cartId = 0)
        {
            VMUserDetail user_details = new VMUserDetail();
            user_details.IsToken = false;
            user_details.IsForgetPassword = isForgetPassword;
            user_details.cartId = cartId;

            if (isForgetPassword == 1)
            {
                return View(user_details);
            }
            else if (!string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")))
            {
                return this.View(user_details);
            }
            else
            {
                return View("Error");
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

            return RedirectToAction("GetUserDetails", new { isForgetPassword = 0, cartId = result.Id });
        }

        [HttpPost]
        public async Task<ActionResult> AuthenticateUserEmail(VMUserDetail email)
        {
            _logger.LogInformation("AuthenticateUserEmail started", new object[] { email });
            try
            {
                if(email.IsForgetPassword == 1)
                {
                    HttpContext.Session.SetString("CurrentUser", email.user_email);
                    HttpContext.Session.CommitAsync().Wait();
                }

                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")))
                {
                    var tokenId = await _shopping.AuthenticateUser(email.user_email);

                    VMUserDetail user_details = new VMUserDetail();
                    user_details.user_email = email.user_email;
                    user_details.userAuthID = tokenId;
                    user_details.IsToken = true;
                    user_details.IsForgetPassword = email.IsForgetPassword;
                    user_details.cartId = email.cartId;
                    ModelState.Clear();
                    return this.View("GetUserDetails", user_details);
                }
                else
                    return View("error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new object[] { email });
                throw ex;
            }
        }

        [HttpPost]
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
                user_details.IsForgetPassword = email.IsForgetPassword;
                user_details.cartId = email.cartId;
                ModelState.Clear();
                return this.View("GetUserDetails", user_details);
            }
            else
                return View("Error");
        }

        [HttpPost]
        public async Task<ActionResult> VerifyTokenAsync(VMUserDetail email)
        {
            _logger.LogInformation("VerifyTokenAsync started", new object[] { email });
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")))
                {
                    bool verificationresult = await _shopping.VerifyUserToken(email.user_email, email.userAuthID, email.Token);

                    if (verificationresult)
                    {
                        if (email.IsForgetPassword > 0)
                        {
                            return RedirectToAction("UpdateUserPasswordView", "User", new { userEmail = _encryption.EncryptText(email.user_email,ATMConstants.emailEncKey) });
                        }
                        else
                        {
                            await _shopping.SendBookingConfirmation(email.user_email, email.cartId);

                            return this.View(verificationresult);
                        }
                    }
                    else
                    {

                        VMUserDetail user_details = new VMUserDetail();
                        user_details.user_email = email.user_email;
                        user_details.IsToken = true;
                        user_details.Message = "Invalid token";

                        return this.View("GetUserDetails", user_details);
                    }
                }
                else
                    return View("Error");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message, new object[] { email });
                throw ex;
            }


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

            return View(result);
        }

        //TODO : create view -> go to cart / continue shopping (home)
        public async Task<ActionResult> AddToCart(int activityId)
        {
            string userEmail = string.Empty;

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")))
            {
                userEmail = HttpContext.Session.GetString("CurrentUser");
            }

            var result = await _shopping.AddToCart(activityId, userEmail);

            var vmObj = ActivityCartDTO.TransformcartItem(result, activityId);
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