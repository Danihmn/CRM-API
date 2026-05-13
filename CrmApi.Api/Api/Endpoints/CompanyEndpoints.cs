namespace CrmApi.Api.Api.Endpoints
{
    public static class CompanyEndpoints
    {
        public static void MapCompanyEndpoints (this WebApplication app)
        {
            var group = app.MapGroup("/companies").WithTags("Companies");

            group.MapGet("/", async (ICompanyService companyService) =>
            {
                var companies = await companyService.GetAllAsync();
                return Results.Ok(companies);
            }).WithDescription("Retorna todas as empresas")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager", "Viewer"));

            group.MapGet("/segment/{segment}", async (string segment, ICompanyService companyService) =>
            {
                var company = await companyService.GetBySegmentAsync(segment);
                return company is not null ? Results.Ok(company) : Results.NotFound();
            }).WithDescription("Retorna empresa pelo segmento")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager", "Viewer"));

            group.MapGet("/id/{id}", async (int id, ICompanyService companyService) =>
            {
                var company = await companyService.GetByIdAsync(id);
                return company is not null ? Results.Ok(company) : Results.NotFound();
            }).WithDescription("Retorna empresa pelo ID")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager", "Viewer"));

            group.MapGet("/cnpj/{cnpj}", async (string cnpj, ICompanyService companyService) =>
            {
                var company = await companyService.GetByCNPJAsync(cnpj);
                return company is not null ? Results.Ok(company) : Results.NotFound();
            }).WithDescription("Retorna empresa pelo CNPJ")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager", "Viewer"));

            group.MapPost("/", async (CompanyRequestDTO company, ICompanyService service) =>
            {
                await service.CreateAsync(company);
                return Results.Created($"/companies/cnpj/{company.CNPJ}", company);
            }).WithDescription("Cria nova empresa")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager"));

            group.MapPut("/{id}", async (int id, CompanyRequestDTO company, ICompanyService service) =>
            {
                await service.UpdateAsync(id, company);
                return Results.Ok(company);
            }).WithDescription("Atualiza empresa existente")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager"));

            group.MapDelete("/{id}", async (int id, ICompanyService service) =>
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            }).WithDescription("Deleta empresa existente")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager"));
        }
    }
}
