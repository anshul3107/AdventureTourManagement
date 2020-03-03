using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using ADSM.Models;


namespace ADSM
{
    public class ADSMDbContext : DbContext
    {
        public ADSMDbContext()
            : base("name = ADSMDbContext") // Name of Connection string

        {
            // Get the ObjectContext related to this DbContext
            var objectcontext = (this as IObjectContextAdapter).ObjectContext;

            objectcontext.CommandTimeout = 120;
        }


        /// <param name="modelBuilder"></param>

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Stop lazy loading
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ProxyCreationEnabled = true;  // Error: A circular reference was detected while serializing an object of type 'System.Data.Entity.DynamicProxies

            // App schema
            modelBuilder.Entity<Models.User_Details>().ToTable("User_Details");
            //modelBuilder.Entity<Models.Booking_details>().ToTable("Booking_details");
            //modelBuilder.Entity<Models.Package_details>().ToTable("Package_details");
            //modelBuilder.Entity<Models.Region>().ToTable("Region");
            //modelBuilder.Entity<Models.Activities>().ToTable("Activities");
            //modelBuilder.Entity<Models.Activity_package>().ToTable("Activity_package");
            //modelBuilder.Entity<Models.Activity_region>().ToTable("Activity_region");

        }

        public DbSet<User_Details> Users { get; set; }
        //public DbSet<Booking_details> Bookings { get; set; }
        //public DbSet<Package_details> Packages { get; set; }
        //public DbSet<Region> Regions { get; set; }
        //public DbSet<Activities> Activities { get; set; }
        //public DbSet<Activity_package> ActPackages { get; set; }
        //public DbSet<Activity_region> ActRegion { get; set; }
    }
}