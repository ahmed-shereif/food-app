using Domain.Models;
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

       //DB Sets
        public DbSet<Recipe> Recipes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
                  //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FoodAppSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=true;")
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
        public DbSet<User> Users { get; set; }
    }
}
