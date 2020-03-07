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
            : base("name = ADSMDbContext") 

        {
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
            this.Configuration.ProxyCreationEnabled = true;
            // App schema
            modelBuilder.Entity<Models.User_Details>().ToTable("User_Details");
            modelBuilder.Entity<Models.Activities>().ToTable("Activity");
            modelBuilder.Entity<Models.ActivityRatings>().ToTable("ActivityRating");

        }

        public DbSet<User_Details> Users { get; set; }
        public DbSet<Activities> Activities { get; set; }
        public DbSet<ActivityRatings> ActivityRatings { get; set; }
    }
}