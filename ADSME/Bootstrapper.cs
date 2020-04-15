using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using ADSM.Controllers;
using ADSM.Interface;
using ADSM.Models.GuestUser;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace ADSM
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IActivityService, RecentlyBoughtActivities>("A");
            container.RegisterType<IActivityService, RecommendedActivities>("B");
            container.RegisterType<IActivityService, SeasonalActivities>("C");

            config.DependencyResolver = new UnityResolver(container);

            // Other Web API configuration not shown.
        }


        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();      

            container.RegisterType<IActivityService, RecentlyBoughtActivities>("A");
            container.RegisterType<IActivityService, RecommendedActivities>("B");
            container.RegisterType<IActivityService, SeasonalActivities>("C");
            return container;
        }
    }
}