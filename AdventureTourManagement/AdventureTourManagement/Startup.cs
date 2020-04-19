using System;
using System.Collections.Generic;
using AdventureTourManagement.Interface;
using AdventureTourManagement.Interface.Shopping;
using AdventureTourManagement.Interface.User;
using AdventureTourManagement.Models;
using AdventureTourManagement.Models.GuestUser;
using AdventureTourManagement.Models.Shopping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecureAccess.Helper;

namespace AdventureTourManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ATMDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ATMContext")));

            services.AddScoped<RecentlyBoughtActivities>();
            services.AddScoped<SeasonalActivities>();
            services.AddScoped<RecommendedActivities>();

            services.AddScoped<IShopping, ShoppingService>();

            services.AddScoped<Func<string,IActivityService>>(serviceProvider => key =>
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
            services.AddSecureAccessService();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureTourManagement.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(60);
                options.Cookie.IsEssential = true;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddSessionStateTempDataProvider();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
