
using Application.Helpers.MappingProfile;
using AutoMapper;
using Domain.Contracts;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Authentication;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Presentation.MiddleWares;
using Presentation.OptionsSetup;
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
            builder.Services.AddAutoMapper(typeof(Program));

        

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            builder.Services.ConfigureOptions<JwtOptionsSetup>();

           
          


           // builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepository<>));
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();
            builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();


            //builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegistrationCommandHandler).Assembly));
            //  builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Application.AssemblyMarker).Assembly));
            builder.Services.AddAutoMapper(
                typeof(Program).Assembly,
                typeof(Application.AssemblyMarker).Assembly);
  

             builder.Services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepository<>));
         

      

            var app = builder.Build();



            // Add the global exception middleware
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseMiddleware<TransactionMiddleware>();

            AutoMapperService.Mapper = app.Services.GetService<IMapper>();




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
