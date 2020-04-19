using AdventureTourManagement.Interface;
using AdventureTourManagement.Interface.Shopping;
using AdventureTourManagement.Interface.User;
using AdventureTourManagement.Models.GuestUser;
using AdventureTourManagement.Models.Shopping;
using AdventureTourManagement.Utility;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace AdventureTourManagement.Models.Shared
{
    public static class ServiceCollectionExtension
    {
        public static void AddServiceConfiguration(this IServiceCollection services)
        {
            services.AddScoped<RecentlyBoughtActivities>();
            services.AddScoped<SeasonalActivities>();
            services.AddScoped<RecommendedActivities>();

            services.AddScoped<IShopping, ShoppingService>();
            services.AddScoped<IConnect, ConnectService>();
            services.AddScoped<Func<string, IActivityService>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case "RA":
                        return serviceProvider.GetService<RecommendedActivities>();
                    case "RB":
                        return serviceProvider.GetService<RecentlyBoughtActivities>();
                    case "SA":
                        return serviceProvider.GetService<SeasonalActivities>();
                    default:
                        throw new KeyNotFoundException(); // or maybe return null, up to you
                }
            });

            services.AddScoped<IActivityAction, ActivityModule>();
            services.AddScoped<IUser, UserService>();
        }
    }
}
