
using AutoMapper;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // **Register DbContext**
            //builder.Services.AddDbContext<Context>(options =>
            //             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            //                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            //                .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
            //                .EnableSensitiveDataLogging()
            //             );



            builder.Services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepository<>));
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
