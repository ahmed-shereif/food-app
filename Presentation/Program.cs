
using AutoMapper;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Presentation.MiddleWares;
using System.Diagnostics;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<Context>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                   .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                   .EnableSensitiveDataLogging()
                   .LogTo(log => Debug.WriteLine(log), LogLevel.Information));


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            var app = builder.Build();


            // Add the global exception middleware
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseMiddleware<TransactionMiddleware>();
            // Other middleware




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
