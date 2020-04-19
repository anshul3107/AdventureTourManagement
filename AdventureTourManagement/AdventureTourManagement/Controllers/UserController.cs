using System.Linq;
using System.Threading.Tasks;
using AdventureTourManagement.Interface;
using AdventureTourManagement.Interface.Shopping;
using AdventureTourManagement.Interface.User;
using AdventureTourManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdventureTourManagement.Controllers
{
    public class UserController : Controller
    {
        private IUser _userService;
        private IShopping _shoppingService;
        private IActivityAction _activityService;

        public UserController(IUser userService, IShopping shoppingService, IActivityAction activityService)
        {
            _userService = userService;
            _shoppingService = shoppingService;
            _activityService = activityService;
        }
             
        public IActionResult Index()
        {
            ModelState.Clear();
            return View();
        }

        private async Task<VmUser> GetUserProfile(string userEmail)
        {
            ModelState.Clear();
            VmUser user = await _userService.GetUserProfile(userEmail);
            return user;
        }

        public async Task<IActionResult> GetBookingHistory(string userEmail)
        {
            ModelState.Clear();
            var bookingHistory = await _shoppingService.FetchAllOrders(userEmail);
            VmBookinglist bookings = new VmBookinglist() { Bookings = bookingHistory };
            return View(bookings);
        }

        public async Task<IActionResult> LoginUser(VmUser userLogin)
        {
            ModelState.Clear();
            var lResult = await _userService.ValidateUserLogin(userLogin);
            return View(lResult);
        }

        public IActionResult RegisterNewUserView()
        {
            ModelState.Clear();
            return View();
        }

        public async Task<IActionResult> RegisterNewUser(VmUser userDets)
        {
            ModelState.Clear();
            var result = await _userService.AddNewUser(userDets);
            return View("LoginUser",result);
        }

        public async Task<IActionResult> UpdateUserDetailView(string userEmail)
        {
            ModelState.Clear();
            var user = await GetUserProfile(userEmail);
            return View(user);
        }

        public async Task<IActionResult> UpdateUserDetail(VmUser userDets)
        {
            ModelState.Clear();
            var result = await _userService.UpdateUserDetails(userDets);
            return View("LoginUser",result);
        }

        public async Task<IActionResult> ForgotPassword()
        {

            return RedirectToAction("GetUserDetails","Shop", new { isForgetPassword = 1 });
        }

        public async Task<IActionResult> UpdateUserPasswordView(string userEmail)
        {
            ModelState.Clear();

            var user = await GetUserProfile(userEmail);
            return View(user);
        }

        public async Task<IActionResult> UpdateUserPassword(VmUser userDets)
        {
            ModelState.Clear();
            var result = await _userService.UpdatePassword(userDets);
            return View("Index");
        }

        public async Task<IActionResult> ProvideFeedback(VMActivityRating activityRating)
        {
            ModelState.Clear();
            await _userService.ProvideFeedback(activityRating.activity_id, activityRating.activity_rating);
            return RedirectToAction(nameof(GetBookingHistory),new { userEmail = activityRating.UserEmail });
        }

        public IActionResult ProvideFeedbackView(int activity_id, string userEmail)
        {
            ModelState.Clear();
            var activities =  _activityService.GetActivityDetailByID(activity_id);
            VMActivityRating vMActivity = new VMActivityRating()
            {
                activity_id = activities.activity_id,
                ActivityName = activities.activity_name,
                UserEmail = userEmail
            };
            return View(vMActivity);
        }
    }
}