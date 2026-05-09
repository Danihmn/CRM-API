namespace CrmApi.Api.Infrastructure.Configuration
{
    public static class AuthConfiguration
    {
        public static IServiceCollection AddAuthConfiguration (this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfig = configuration.GetSection("Jwt").Get<TokenConfiguration>()!;

            services.AddSingleton(tokenConfig);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenConfig.Issuer,
                        ValidAudience = tokenConfig.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.SecretKey)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}
