namespace CrmApi.Api.Infrastructure.Extensions
{
    public static class EndpointsExtension
    {
        public static WebApplication MapEndpoits (this WebApplication app)
        {
            app.MapAuthEndpoints();
            app.MapCompanyEndpoints();
            app.MapContactEndpoints();
            app.MapContractEndpoints();

            return app;
        }
    }
}
