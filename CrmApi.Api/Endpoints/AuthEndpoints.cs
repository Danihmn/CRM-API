namespace CrmApi.Api.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints (this WebApplication app)
        {
            var group = app.MapGroup("/auth").WithTags("Authentication");

            group.MapPost("/register", async (RegisterRequestDTO request, IAuthService authService) =>
            {
                var token = await authService.RegisterAsync(request);
                return Results.Ok(token);
            }).WithDescription("Registra novo usuário e retorna Token");

            group.MapPost("/login", async (LoginRequestDTO request, IAuthService authService) =>
            {
                var token = await authService.LoginAsync(request);
                return Results.Ok(token);
            }).WithDescription("Autentica usuário e retorna Token");
        }
    }
}
