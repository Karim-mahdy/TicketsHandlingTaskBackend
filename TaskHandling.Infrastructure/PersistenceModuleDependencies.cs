using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Common.Abstraction.Factory;
using TicketsHandling.Application.Common.Abstraction.Repositories;
 
using TicketsHandling.Persistence.Context;
using TicketsHandling.Persistence.Repository;

namespace TicketsHandling.Persistence
{

    public static class PersistenceModuleDependencies
    {
        public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            //services.AddHangfire(config => config.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));
            //services.AddHangfireServer();
            services.AddHangfire(config =>
             config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
              .UseSimpleAssemblyNameTypeSerializer()
              .UseRecommendedSerializerSettings()
              .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
              {
                  CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                  SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                  QueuePollInterval = TimeSpan.Zero,
                  UseRecommendedIsolationLevel = true,
                  DisableGlobalLocks = true
              }));
             services.AddHangfireServer();
          
            return services;
        }
    }
}
