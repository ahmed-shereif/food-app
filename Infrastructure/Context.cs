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
        public DbSet<Category> Categories { get; set; }




     
        public DbSet<User> Users { get; set; }
    }
}
