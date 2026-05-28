var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigurations(builder.Configuration);
builder.Services.AddAuthDependencies();
builder.Services.AddRepositories();
builder.Services.AddServices();

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
app.MapEndpoints();
app.UseScalarDocumentation();
app.Run();
