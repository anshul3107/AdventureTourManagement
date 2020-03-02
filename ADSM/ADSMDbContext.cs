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

        /// <summary>
        /// Set database first options
        /// The model backing the 'GPWorldDbContext' context has changed since the database was created. Consider using Code First Migrations to update the database (http://go.microsoft.com/fwlink/?LinkId=238269)
        /// </summary>
        /// <param name="modelBuilder"></param>
        ///

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

        }

        public DbSet<User_Details> Users { get; set; }
    }
}