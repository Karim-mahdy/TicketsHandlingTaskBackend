
 
namespace TicketsHandling.Persistence
{
    public static class DatabaseModuleDependencies
    {
        public static IServiceCollection AddDatabaseDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var connectioString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectioString));
            services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            return services;
        }
    }
}
