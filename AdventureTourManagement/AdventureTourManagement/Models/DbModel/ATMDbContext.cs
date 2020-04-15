using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADSM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ADSM
{
    public class ATMDbContext : DbContext
    {
        public ATMDbContext(DbContextOptions<ATMDbContext> options)
            : base(options) 

        {
           
        }

        /// <param name="modelBuilder"></param>

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // App schema
            modelBuilder.Entity<Models.Activities>().ToTable("Activity");
            modelBuilder.Entity<Models.ActivityRatings>().ToTable("ActivityRating");
            modelBuilder.Entity<Models.Bookings>().ToTable("Booking");
            modelBuilder.Entity<Models.Packages>().ToTable("Package");
            modelBuilder.Entity<Models.Regions>().ToTable("Region");

        }

        public DbSet<Activities> Activities { get; set; }
        public DbSet<ActivityRatings> ActivityRatings { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
    }
}