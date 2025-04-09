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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entities here
            // For example:
            // modelBuilder.Entity<YourEntity>().ToTable("YourTableName");
        }
    }
}
