using CrmApi.Api.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

#region Configuration DI
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddAuthConfiguration(builder.Configuration);
#endregion

#region Auth DI
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
#endregion

#region Repositories DI
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
#endregion

#region Services DI
builder.Services.AddScoped<ICompanyService, CompanyService>();
#endregion

builder.Services.AddAuthorization();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapAuthEndpoints();
app.MapCompanyEndpoints();

app.UseScalarDocumentation();

app.Run();
