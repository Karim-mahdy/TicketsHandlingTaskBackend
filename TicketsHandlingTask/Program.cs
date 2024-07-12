
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TicketsHandling;

using TicketsHandling.Application;
using TicketsHandling.Application.Hubs;
using TicketsHandling.Domain;
using TicketsHandling.Persistence;
using TicketsHandling.Persistence.BackgroundJobs;
using TicketsHandling.Persistence.Context;

namespace TicketsHandlingTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;
            
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            // Add services to the container.
            builder.Services
                .AddApiDependencies(configuration)
                .AddPersistenceDependencies(configuration)
                .AddDatabaseDependencies(configuration)
                .AddDomainDependencies(configuration)
                .AddApplicationDependencies(configuration);

            builder.Configuration.AddJsonFile("lookups.json", optional: false, reloadOnChange: true);

            // Add CORS
            var CORS = "_cors";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(CORS,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")  // Allow your frontend URL
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .AllowCredentials();  // Allow credentials (like cookies or HTTP authentication)
                    });
            });

            var app = builder.Build();


            app.UseSerilogRequestLogging(); // Enable Serilog request logging

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseCors(CORS);
            app.UseRouting();
            app.UseAuthorization();
            // Use Hangfire dashboard
            app.UseHangfireDashboard("/hangfire");

            // Map controllers
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<TicketHub>("/tickethub");
                endpoints.MapHangfireDashboard("/hangfire");  // Specify the path for the Dashboard
            });

            // Migrate any database changes on startup (includes initial db creation)
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
                 
            }

            // Configure Hangfire Dashboard and Jobs
            BackgroundJob.Enqueue<TicktesJob>(job => job.UpdateTicketStatuses());
            RecurringJob.AddOrUpdate<TicktesJob>(job => job.UpdateTicketStatuses(), Cron.Minutely());
          
            app.Run();
        }
    }
}
