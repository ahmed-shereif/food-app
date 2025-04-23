using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Context : DbContext
    {
        public Context() : base()
        {
        }
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RecipeUserFavorites>().HasQueryFilter(b => !b.IsDeleted);

        }
        //DB Sets
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeUserFavorites> RecipeUserFavorites { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
