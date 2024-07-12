
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Behavior;
using TicketsHandling.Application.Common.SharedModels;

namespace TicketsHandling.Application
{
    public static class ApplicationModuleDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(config => config.RegisterServicesFromAssemblies(assembly));


            // Register FluentValidation validators
            services.AddValidatorsFromAssembly(assembly);

            // Register the pipeline behavior for validation
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddAutoMapper(assembly);
            services.Configure<LookupsData>(configuration.GetSection("LookupsData"));
            // Add SignalR
            services.AddSignalR();

            services.AddSerilog();

            return services;
        }
    }
}
