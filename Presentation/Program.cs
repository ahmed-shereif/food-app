
using Application.CQRS.Recipes.Commands;
using Application.Helpers.MappingProfile;
using AutoMapper;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Presentation.MiddleWares;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // **Register DbContext**
            builder.Services.AddDbContext<Context>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                           .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                           .EnableSensitiveDataLogging()
                        );

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
           
          



             builder.Services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepository<>));
         

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            builder.Services.AddAutoMapper(typeof(Program));
            var app = builder.Build();



            // Add the global exception middleware
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseMiddleware<TransactionMiddleware>();

           // AutoMapperService.Mapper = app.Services.GetService<IMapper>();
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
