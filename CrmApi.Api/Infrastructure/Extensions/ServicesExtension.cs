namespace CrmApi.Api.Infrastructure.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices (this IServiceCollection services)
        {
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IContractService, ContractService>();

            return services;
        }
    }
}
