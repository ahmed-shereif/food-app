using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Domain.Models.BaseModel> BaseModels { get; set; } // Example DbSet, replace with your actual entities

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //  optionsBuilder.UseSqlServer(@"Data source =(localdb)\MSSQLLocalDB; initial catalog = HotelManagementSystem; integrated security = true; trust server certificate=true")
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=foodApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=true;")
                 .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                 .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                 .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entities here
            // For example:
            // modelBuilder.Entity<YourEntity>().ToTable("YourTableName");
        }
    }
}
