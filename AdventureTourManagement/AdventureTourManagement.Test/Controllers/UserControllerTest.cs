using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;
using AdventureTourManagement.Interface.User;
using AdventureTourManagement.Interface.Shopping;
using AdventureTourManagement.Interface;
using AdventureTourManagement.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdventureTourManagement.ViewModels;
using AdventureTourManagement.Models;
using SecureAccess;
using AdventureTourManagement.Utility;

namespace AdventureTourManagement.Test.Controllers
{
   public class UserControllerTest
    {
        private Mock<IUser> _userService;
        private Mock<IShopping> _shoppingService;
        private Mock<IActivityAction> _activityService;
        UserController controller;
        private Mock<IServiceProvider> _provider;

        public UserControllerTest()
        {
            _userService = new Mock<IUser>();
            _shoppingService = new Mock<IShopping>();
            _activityService = new Mock<IActivityAction>();
            _provider = new Mock<IServiceProvider>();

            _provider.Setup(x => x.GetService(typeof(TwoStepAuth))).Returns(new TwoStepAuth());

            controller = new UserController(_userService.Object, _shoppingService.Object, _activityService.Object, _provider.Object);
        }


        [Fact]
        public async Task Index_success()
        {
            //act
            var result = controller.Index() as ViewResult;

            //Assert 
            Assert.NotNull(result);
        }

        private string EncryptedUserEmail()
        {
            TwoStepAuth testObj = new TwoStepAuth();
            var encryption = testObj.GetEncryptionDecryption;
            string result = encryption.EncryptText("test@email.com", ATMConstants.emailEncKey);
            return result;
        }

        [Fact]
        public async Task GetBookingHistory_success()
        {
            //arrange
            List<VmBooking> lst = new List<VmBooking>()
            {
                new VmBooking()
                {
                    ActivityId = 1,
                    ActivityName = "test",
                    ActivityDesc = "test",
                    ActivityFee = 12,
                    ActivityImage="test//test",
                    BookingDate=DateTime.Now,
                    UserEmail = "test.com"
                }
            };

            _shoppingService.Setup(x => x.FetchAllOrders(It.IsAny<string>())).Returns(Task.FromResult(lst));
            var userEmailEnc = EncryptedUserEmail();

            //act
            var result = await controller.GetBookingHistory(userEmailEnc) as ViewResult;

            //Assert 
            Assert.NotNull(result.Model);
        }

        [Fact]
        public async Task LoginUser_success()
        {
            //arrange
            VmUser user = new VmUser
            {
                UserName = "test",
                UserContact = "123",
                UserEmail = "test123",
                UserEncryptionMessage = "test",
                UserEncyryptedKey = "test"
            };
            _userService.Setup(x => x.ValidateUserLogin(It.IsAny<VmUser>())).Returns(Task.FromResult(user));

            //act
            var result = await controller.LoginUser(user) as ViewResult;

            //assert
            Assert.NotNull(result.Model);
        }

        [Fact]
        public async Task RegisterNewUserView_success()
        {
            //act
            var result = controller.RegisterNewUserView() as ViewResult;

            //Assert 
            Assert.NotNull(result);
        }

        [Fact]
        public async Task RegisterNewUser_success()
        {
            //arrange
            VmUser user = new VmUser
            {
                UserName = "test",
                UserContact = "123",
                UserEmail = "test123",
                UserEncryptionMessage = "test",
                UserEncyryptedKey = "test"
            };
            _userService.Setup(x => x.AddNewUser(It.IsAny<VmUser>())).Returns(Task.FromResult(user));

            //act
            var result = await controller.RegisterNewUser(user) as ViewResult;

            //assert
            Assert.NotNull(result.Model);
        }

        [Fact]
        public async Task UpdateUserDetailView_success()
        {
            //arrange 
            VmUser user = new VmUser
            {
                UserName = "test",
                UserContact = "123",
                UserEmail = "test123",
                UserEncryptionMessage = "test",
                UserEncyryptedKey = "test"
            };

            _userService.Setup(x => x.GetUserProfile(It.IsAny<string>())).Returns(Task.FromResult(user));

            var userEmailEnc = EncryptedUserEmail();
            //act
            var result =await controller.UpdateUserDetailView(userEmailEnc) as ViewResult;

            //Assert 
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateUserDetail_success()
        {
            //arrange 
            VmUser user = new VmUser
            {
                UserName = "test",
                UserContact = "123",
                UserEmail = "test123",
                UserEncryptionMessage = "test",
                UserEncyryptedKey = "test"
            };

            _userService.Setup(x => x.UpdateUserDetails(It.IsAny<VmUser>())).Returns(Task.FromResult(user));

            //act
            var result = await controller.UpdateUserDetail(user) as ViewResult;

            //Assert 
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateUserPasswordView_success()
        {
            //arrange 
            VmUser user = new VmUser
            {
                UserName = "test",
                UserContact = "123",
                UserEmail = "test123",
                UserEncryptionMessage = "test",
                UserEncyryptedKey = "test"
            };

            _userService.Setup(x => x.GetUserProfile(It.IsAny<string>())).Returns(Task.FromResult(user));
            var userEmailEnc = EncryptedUserEmail();

            //act
            var result = await controller.UpdateUserPasswordView(userEmailEnc) as ViewResult;

            //Assert 
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateUserPassword_success()
        {
            //arrange 
            VmUser user = new VmUser
            {
                UserName = "test",
                UserContact = "123",
                UserEmail = "test123",
                UserEncryptionMessage = "test",
                UserEncyryptedKey = "test"
            };

            _userService.Setup(x => x.UpdatePassword(It.IsAny<VmUser>())).Returns(Task.FromResult(user));

            //act
            var result = await controller.UpdateUserPassword(user) as ViewResult;

            //Assert 
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ProvideFeedback_success()
        {
            //arrange
            VMActivityRating rating = new VMActivityRating()
            {
                activity_id = 1,
                ActivityName = "test",
                activity_rating = 3,
                UserEmail = "test"
            };
            _userService.Setup(x => x.ProvideFeedback(It.IsAny<int>(), It.IsAny<int>()));

            //act
            var result = await controller.ProvideFeedback(rating) as RedirectToActionResult;

            //assert
            Assert.Equal("GetBookingHistory", result.ActionName);
        }

        [Fact]
        public async Task ProvideFeedbackView_success()
        {
            //arrange
            Activities activity = new Activities()
            {
                activity_id = 1,
                activity_name = "test",
                activity_fee = 12,
                activity_description = "tesrt desc",
                activity_image_path = "test//test1",
                activity_slots = 12
            };

            _activityService.Setup(x => x.GetActivityDetailByID(1)).Returns(activity);
            var userEmailEnc = EncryptedUserEmail();

            //act
            var result = controller.ProvideFeedbackView(1, userEmailEnc) as ViewResult;

            //assert
            Assert.NotNull(result.Model);

        }
    }
}
