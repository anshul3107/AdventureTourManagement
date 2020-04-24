using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureTourManagement.Controllers;
using AdventureTourManagement.Interface.Shopping;
using AdventureTourManagement.Models;
using AdventureTourManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using SecureAccess;
using Xunit;

namespace AdventureTourManagement.Test.Controllers
{
    public class ShopControllerTest
    {
        Mock<IShopping> _shopping;
        ShopController controller;
        Mock<IServiceProvider> _provider;
        Mock<ILogger<ShopController>> _logger;


        public ShopControllerTest()
        {
            _shopping = new Mock<IShopping>();
            _provider = new Mock<IServiceProvider>();
            _provider.Setup(x => x.GetService(typeof(TwoStepAuth))).Returns(new TwoStepAuth());
            _logger = new Mock<ILogger<ShopController>>();
            controller = new ShopController(_shopping.Object, _logger.Object, _provider.Object);
        }

        private void mockSession()
        {
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession mockSession = new MockHttpSession();
            mockSession["CurrentUser"] = Guid.NewGuid();
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;
        }

        [Fact]
        public async Task GetUserDetails_Success()
        {
            //Arrange
            mockSession();

            //Act
            var result = controller.GetUserDetails(0, 1) as ViewResult;

            //Assert
            Assert.NotNull(result.Model);
        }

        [Fact]
        public async Task BuyNowAsync_Success()
        {
            //Arrange
            mockSession();
            ActivityCart cart = new ActivityCart()
            {
                Id = 1,
                ItemDetails = JsonConvert.SerializeObject(new ActivityCartDTO()
                {
                    ActivityID = 1,
                    ActivityImage = "test//test",
                    ActivityDescription = "test",
                    ActivityName = "test",
                    ActivityFee = 50,
                    Quantity = 1
                }),
                Username = "test@xyz.com"
            };

            _shopping.Setup(x => x.AddToCart(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(cart));

            //Act
            var result = await controller.BuyNowAsync(1) as RedirectToActionResult;

            //Assert
            Assert.Equal("GetUserDetails", result.ActionName);
        }

        [Fact]
        public async Task AuthenticateUserEmail_Success()
        {
            //Arrange
            mockSession();
            _shopping.Setup(x => x.AuthenticateUser(It.IsAny<string>())).Returns(Task.FromResult(Guid.NewGuid()));
            VMUserDetail input = new VMUserDetail()
            {
                user_email = "test@mail.com",
                IsForgetPassword = 0,
                cartId = 1
            };

            //Act
            var result = await controller.AuthenticateUserEmail(input) as ViewResult;

            //Assert
            Assert.Equal("GetUserDetails", result.ViewName);

        }

        [Fact]
        public async Task ResendAuthToken_Success()
        {
            //Arrange
            mockSession();
            _shopping.Setup(x => x.AuthenticateUser(It.IsAny<string>())).Returns(Task.FromResult(Guid.NewGuid()));
            VMUserDetail input = new VMUserDetail()
            {
                user_email = "test@xyz.com",
                IsForgetPassword = 0,
                cartId = 1
            };

            //Act
            var result = await controller.ResendAuthToken(input) as ViewResult;

            //Assert
            Assert.Equal("GetUserDetails", result.ViewName);

        }

        [Fact]
        public async Task VerifyTokenAsync_tokenvalid_Success()
        {
            //Arrange
            mockSession();

            VMUserDetail input = new VMUserDetail()
            {
                user_email = "test@xyz.com",
                IsForgetPassword = 0,
                cartId = 1
            };

            _shopping.Setup(x => x.VerifyUserToken(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<string>())).Returns(Task.FromResult(true));
            _shopping.Setup(x => x.SendBookingConfirmation(It.IsAny<string>(), It.IsAny<int>()));

            //Act
            var result = await controller.VerifyTokenAsync(input) as ViewResult;

            //Assert
            Assert.NotNull(result.Model);
        }


        [Fact]
        public async Task VerifyTokenAsync_tokeninvalid_Success()
        {
            //Arrange
            mockSession();

            VMUserDetail input = new VMUserDetail()
            {
                user_email = "test@xyz.com",
                IsForgetPassword = 0,
                cartId = 1
            };

            _shopping.Setup(x => x.VerifyUserToken(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<string>())).Returns(Task.FromResult(false));
            _shopping.Setup(x => x.SendBookingConfirmation(It.IsAny<string>(), It.IsAny<int>()));

            //Act
            var result = await controller.VerifyTokenAsync(input) as ViewResult;

            //Assert
            Assert.NotNull(result.Model);
            Assert.Equal("GetUserDetails", result.ViewName);
        }

        [Fact]
        public async Task ViewShoppingCart_Success()
        {
            //Arrange
            mockSession();


            VMActivityCart cartOtpt = new VMActivityCart()
            {
                CartId = 1,
                CartItem = new List<ActivityCartDTO>() { new ActivityCartDTO()
                {
                    ActivityID = 1,
                    ActivityImage = "test//test",
                    ActivityDescription = "test",
                    ActivityName = "test",
                    ActivityFee = 50,
                    Quantity = 1
                }}

            };
            _shopping.Setup(x => x.FetchShoppingCart(It.IsAny<string>())).Returns(Task.FromResult(cartOtpt));

            //Act
            var result = await controller.ViewShoppingCart() as ViewResult;

            //Assert
            Assert.NotNull(result.Model);
        }

        [Fact]
        public async Task AddToCart_Success()
        {
            //Arrange
            mockSession();
            List<ActivityCartDTO> lst = new List<ActivityCartDTO>(){ new ActivityCartDTO()
            {
                ActivityID = 1,
                ActivityImage = "test//test",
                ActivityDescription = "test",
                ActivityName = "test",
                ActivityFee = 50,
                Quantity = 1
            } };
            ActivityCart cart = new ActivityCart()
            {
                Id = 1,
                ItemDetails = JsonConvert.SerializeObject(lst),
                Username = "test@xyz.com"
            };

            _shopping.Setup(x => x.AddToCart(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(cart));

            //Act
            var result = await controller.AddToCart(1) as ViewResult;

            //Assert
            Assert.NotNull(result.Model);
        }

        [Fact]
        public async Task DeleteFromCart_Success()
        {
            //Arrange
            mockSession();
            mockSession();
            List<ActivityCartDTO> lst = new List<ActivityCartDTO>(){ new ActivityCartDTO()
            {
                ActivityID = 1,
                ActivityImage = "test//test",
                ActivityDescription = "test",
                ActivityName = "test",
                ActivityFee = 50,
                Quantity = 1
            } };
            ActivityCart cart = new ActivityCart()
            {
                Id = 1,
                ItemDetails = JsonConvert.SerializeObject(lst),
                Username = "test@xyz.com"
            };

            _shopping.Setup(x => x.RemoveFromCart(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(cart));

            //Act
            var result = await controller.DeleteFromCart(1) as RedirectToActionResult;

            //Assert
            Assert.Equal("ViewShoppingCart", result.ActionName);
        }

    }
}
