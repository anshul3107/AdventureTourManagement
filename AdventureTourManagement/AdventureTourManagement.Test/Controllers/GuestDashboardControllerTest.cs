using AdventureTourManagement.Controllers;
using AdventureTourManagement.Interface;
using AdventureTourManagement.Models;
using AdventureTourManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AdventureTourManagement.Test.Controllers
{
    [ExcludeFromCodeCoverage]
    public class GuestDashboardControllerTest
    {
        Mock<IActivityAction> _service;
        GuestDashboardController controller;
        public GuestDashboardControllerTest()
        {
            _service = new Mock<IActivityAction>();
            controller = new GuestDashboardController(_service.Object);
        }

        [Fact]
        public async Task Index_Success()
        {
            //Arrange
            List<SelectListItem> lstRegions = new List<SelectListItem>()
            {
                new SelectListItem{
                Text="all",
                Value="0"
                },
                new SelectListItem{
                Text="region1",
                Value="1"
                },
                new SelectListItem{
                Text="region2",
                Value="2"
                },
            };
            List<VMActivityDetails> lstActivities = new List<VMActivityDetails>()
            {
                new VMActivityDetails
                {
                    activity_id = 1,
                    activity_name="test",
                    activity_slots = 10,
                    activity_description = "test desc",
                    activity_fee = 500,
                    activity_season = "Winter"
                },
                new VMActivityDetails
                {
                    activity_id = 2,
                    activity_name="test2",
                    activity_slots = 100,
                    activity_description = "test desc 2",
                    activity_fee = 50,
                    activity_season = "Winter"
                }
        };

            IDictionary<string, List<VMActivityDetails>> otpActivities = new Dictionary<string, List<VMActivityDetails>>();
            otpActivities.Add("SA", lstActivities);
            otpActivities.Add("RA", lstActivities);
            otpActivities.Add("RB", lstActivities);

            mockSession();

            _service.Setup(x => x.GetRegions()).Returns(Task.FromResult(lstRegions));
            _service.Setup(x => x.GetActivities(It.IsAny<int>())).Returns(Task.FromResult(otpActivities));

            //Act
            var result = await controller.Index() as ViewResult;

            //Assert
            Assert.NotNull(result.Model);
        }

        [Fact]
        public async Task FilterActivities_withRegionId_Success()
        {

            //Arrange
            List<SelectListItem> lstRegions = new List<SelectListItem>()
            {
                new SelectListItem{
                Text="all",
                Value="0"
                },
                new SelectListItem{
                Text="region1",
                Value="1"
                },
                new SelectListItem{
                Text="region2",
                Value="2"
                },
            };
            List<VMActivityDetails> lstActivities = new List<VMActivityDetails>()
            {
                new VMActivityDetails
                {
                    activity_id = 1,
                    activity_name="test",
                    activity_slots = 10,
                    activity_description = "test desc",
                    activity_fee = 500,
                    activity_season = "Winter"
                },
                new VMActivityDetails
                {
                    activity_id = 2,
                    activity_name="test2",
                    activity_slots = 100,
                    activity_description = "test desc 2",
                    activity_fee = 50,
                    activity_season = "Winter"
                }
        };

            IDictionary<string, List<VMActivityDetails>> otpActivities = new Dictionary<string, List<VMActivityDetails>>();
            otpActivities.Add("SA", lstActivities);
            otpActivities.Add("RA", lstActivities);
            otpActivities.Add("RB", lstActivities);

            mockSession();

            _service.Setup(x => x.GetRegions()).Returns(Task.FromResult(lstRegions));
            _service.Setup(x => x.GetActivities(It.IsAny<int>())).Returns(Task.FromResult(otpActivities));

            VMActivity input = new VMActivity()
            {
                RegionSelected = "1"
            };

            //Act
            var result = await controller.FilterActivities(input) as RedirectToActionResult;

            //Assert
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task FilterActivities_withoutRegionId_Success()
        {

            //Arrange
            List<SelectListItem> lstRegions = new List<SelectListItem>()
            {
                new SelectListItem{
                Text="all",
                Value="0"
                },
                new SelectListItem{
                Text="region1",
                Value="1"
                },
                new SelectListItem{
                Text="region2",
                Value="2"
                },
            };
            List<VMActivityDetails> lstActivities = new List<VMActivityDetails>()
            {
                new VMActivityDetails
                {
                    activity_id = 1,
                    activity_name="test",
                    activity_slots = 10,
                    activity_description = "test desc",
                    activity_fee = 500,
                    activity_season = "Winter"
                },
                new VMActivityDetails
                {
                    activity_id = 2,
                    activity_name="test2",
                    activity_slots = 100,
                    activity_description = "test desc 2",
                    activity_fee = 50,
                    activity_season = "Winter"
                }
        };

            IDictionary<string, List<VMActivityDetails>> otpActivities = new Dictionary<string, List<VMActivityDetails>>();
            otpActivities.Add("SA", lstActivities);
            otpActivities.Add("RA", lstActivities);
            otpActivities.Add("RB", lstActivities);

            mockSession();

            _service.Setup(x => x.GetRegions()).Returns(Task.FromResult(lstRegions));
            _service.Setup(x => x.GetActivities(It.IsAny<int>())).Returns(Task.FromResult(otpActivities));

            VMActivity input = new VMActivity()
            {
                RegionSelected = "0"
            };

            //Act
            var result = await controller.FilterActivities(input) as RedirectToActionResult;

            //Assert
            Assert.Equal("Index",result.ActionName);
        }

        [Fact]
        public async Task FetchActivity_Success()
        {
            //Arrange
            Activities activity = new Activities()
            {
                activity_id = 1,
                activity_name = "test",
                activity_fee = 12,
                activity_description = "tesrt desc",
                activity_image_path = "test//test1",
                activity_slots = 12
            };

            mockSession();
            _service.Setup(x => x.GetActivityDetailByID(1)).Returns(activity);

            //Act
            var result = controller.FetchActivity(1) as ViewResult;

            //Assert
            Assert.NotNull(result.Model);
        }

        [Fact]
        public async Task FetchAllActivity_Success()
        {
            //Arrange
            Activities activity = new Activities()
            {
                activity_id = 1,
                activity_name = "test",
                activity_fee = 12,
                activity_description = "tesrt desc",
                activity_image_path = "test//test1",
                activity_slots = 12
            };

            List<Activities> lst = new List<Activities> { activity };
            mockSession();

            _service.Setup(x => x.GetAllActivities(It.IsAny<int>())).Returns(Task.FromResult(lst));

            //Act
            var result = await controller.FetchAllActivity() as ViewResult;

            //Assert
            Assert.NotNull(result.Model);
        }

        private void mockSession()
        {
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession mockSession = new MockHttpSession();
            mockSession["CurrentUser"] = Guid.NewGuid();
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;
        }
    }

    public class MockHttpSession : ISession
    {
        Dictionary<string, object> sessionStorage = new Dictionary<string, object>();

        public object this[string name]
        {
            get { return sessionStorage[name]; }
            set { sessionStorage[name] = value; }
        }

        string ISession.Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool ISession.IsAvailable
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        IEnumerable<string> ISession.Keys
        {
            get { return sessionStorage.Keys; }
        }

        void ISession.Clear()
        {
            sessionStorage.Clear();
        }


        void ISession.Remove(string key)
        {
            sessionStorage.Remove(key);
        }

        void ISession.Set(string key, byte[] value)
        {
            sessionStorage[key] = value;
        }

        bool ISession.TryGetValue(string key, out byte[] value)
        {
            if (sessionStorage[key] != null)
            {
                value = Encoding.ASCII.GetBytes(sessionStorage[key].ToString());
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        public async Task LoadAsync(CancellationToken cancellationToken = default)
        {
            await test();
           // throw new NotImplementedException();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {

            await test();
            // throw new NotImplementedException();
        }

        public async Task test()
        {
            int i;
        }
    }
}
