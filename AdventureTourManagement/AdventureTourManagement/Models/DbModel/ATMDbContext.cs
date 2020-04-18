using AdventureTourManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureTourManagement
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
            modelBuilder.Entity<Models.UserDetails>().ToTable("UserDetail");
            modelBuilder.Entity<Models.ActivityCart>().ToTable("ActivityCart");

        }

        public DbSet<Activities> Activities { get; set; }
        public DbSet<ActivityRatings> ActivityRatings { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Packages> Packages { get; set; }
        public DbSet<Regions> Regions { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<ActivityCart> ActivityCart { get; set; }

    }
}