namespace CrmApi.Api.Infrastructure.Extensions
{
    public static class AuthExtension
    {
        public static IServiceCollection AddAuthDependencies (this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
