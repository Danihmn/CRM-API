namespace CrmApi.Api.Infrastructure.Extensions
{
    public static class ConfigurationsExtension
    {
        public static IServiceCollection AddConfigurations
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseConfiguration(configuration);
            services.AddAuthConfiguration(configuration);

            return services;
        }
    }
}
