namespace CrmApi.Api.Infrastructure.Extensions
{
    public static class RepositoriesExtension
    {
        public static IServiceCollection AddRepositories (this IServiceCollection services)
        {
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IContractRepository, ContractRepository>();

            return services;
        }
    }
}
