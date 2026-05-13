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
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
#endregion

#region Services DI
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IContractService, ContractService>();
#endregion

builder.Services.AddAuthorization();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

#region Map Endpoints
app.MapAuthEndpoints();
app.MapCompanyEndpoints();
app.MapContactEndpoints();
app.MapContractEndpoints();
#endregion

app.UseScalarDocumentation();

app.Run();
