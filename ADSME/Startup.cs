using ADSM.Interface;
using ADSM.Models.GuestUser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(ADSM.Startup))]

namespace ADSM
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var resolver = new DefaultDependencyResolver(services.BuildServiceProvider());
            DependencyResolver.SetResolver(resolver);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<RecentlyBoughtActivities>();
            services.AddScoped<RecommendedActivities>();
            services.AddScoped<SeasonalActivities>();

            services.AddScoped<Func<string, IActivityService>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case "A":
                        return serviceProvider.GetService<RecentlyBoughtActivities>();
                    case "B":
                        return serviceProvider.GetService<RecommendedActivities>();
                    case "C":
                        return serviceProvider.GetService<SeasonalActivities>();
                    default:
                        return null;
                }
            });

            services.AddControllersAsServices(typeof(Startup).Assembly.GetExportedTypes()
            .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
            .Where(t => typeof(IController).IsAssignableFrom(t)
            || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)));


        }

    }
}
