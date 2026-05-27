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
builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
builder.Services.AddTransient<IContactRepository, ContactRepository>();
builder.Services.AddTransient<IContractRepository, ContractRepository>();
#endregion

#region Services DI
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IContractService, ContractService>();
#endregion

#region .NET Pipelines
builder.Services.AddAuthorization();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddOpenApi();
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseExceptionHandler();
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
