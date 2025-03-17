
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using RailwayReservationManagementSystem.Interfaces;
using RailwayReservationManagementSystem.Models;
using RailwayReservationManagementSystem.Repositories;
using RailwayReservationManagementSystem.Services;
using System.Text;
using IUser = RailwayReservationManagementSystem.Interfaces.IUser;
//using UserService = RailwayReservationManagementSystem.Services.AuthenticationService;

namespace RailwayReservationManagementSystem
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add JWT authentication
          
            // Add services to the container.
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            


            builder.Services.AddDbContext<RailwayReservationManagementSystemContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            //add services 

            // cors for integrating in angular
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    builder => builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>{
        var jwtSettings = builder.Configuration.GetSection("Jwt");

        options.RequireHttpsMetadata = false; // Set this to true if using HTTPS
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
        };
    });
            // Add Authorization Services
            builder.Services.AddAuthorization();

            // Add other services like your repositories, application services, etc.
            builder.Services.AddSingleton<JwtTokenHelper>();

            builder.Services.AddScoped<ITrainRepo, TrainRepo>();
            builder.Services.AddScoped<IPassengerRepo, PassengerRepo>();
            //builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();
            builder.Services.AddScoped<IAdministratorRepo, AdministratorRepo>();
            builder.Services.AddScoped<IReservationRepo, ReservationRepo>();
            //builder.Services.AddScoped<IAuthentication, AuthenticationService>();
            builder.Services.AddScoped<IUser, UserService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAngularApp");
            app.UseHttpsRedirection();
            //app.UseAuthentication();
            //app.UseAuthorization();


            app.MapControllers();

            app.Run();

        }
    }
}
