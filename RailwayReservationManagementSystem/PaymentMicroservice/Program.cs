
using Microsoft.EntityFrameworkCore;
using PaymentMicroservice.Models;
using PaymentMicroservice.Repositories;

namespace PaymentMicroservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //CreateHostBuilder(args).Build().Run();

            // Add services to the container.
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();



            builder.Services.AddDbContext<RailwayReservationManagementSystemContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<PaymentService>();
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //Host.CreateDefaultBuilder(args)
        //    .ConfigureWebHostDefaults(webBuilder =>
        //    {
        //        webBuilder.ConfigureServices(services =>
        //        {
        //            // Register DbContext
        //            services.AddDbContext<RailwayReservationManagementSystemContext>(options =>
        //                options.UseSqlServer("DefaultConnection"));

        //            // Register PaymentService
        //            services.AddScoped<PaymentService>();
        //            services.AddControllers();
        //        });
        //        webBuilder.Configure(app =>
        //        {
        //            app.UseRouting();
        //            app.UseEndpoints(endpoints =>
        //            {
        //                endpoints.MapControllers();
        //            });
        //        });
        //    });
    }
}
