namespace CrmApi.Api.Infrastructure.Configuration
{
    public static class ScalarConfiguration
    {
        public static WebApplication UseScalarDocumentation (this WebApplication app)
        {
            app.MapScalarApiReference("/scalar", options =>
            {
                options
                    .WithTitle("CRM API")
                    .WithOpenApiRoutePattern("/openapi/v1.json");
            });

            return app;
        }
    }
}
