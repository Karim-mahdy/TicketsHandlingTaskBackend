﻿using System.Reflection;
using TicketsHandling.API.Filters;

namespace TicketsHandling
{
    public static class ApiModuleDependencies
    {
        public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddControllers(options =>
            {
                options.Filters.Add<CustomExceptionFilterAttribute>();
            });
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(assembly));
            return services;
        }
    }
}
