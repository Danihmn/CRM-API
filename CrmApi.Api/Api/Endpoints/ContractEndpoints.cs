namespace CrmApi.Api.Api.Endpoints
{
    public static class ContractEndpoints
    {
        public static void MapContractEndpoints (this WebApplication app)
        {
            var group = app.MapGroup("/contracts").WithTags("Contracts");

            group.MapGet("/", async (IContractService contractService) =>
            {
                var contracts = await contractService.GetAllAsync();
                return Results.Ok(contracts);
            }).WithDescription("Retorna todos os contratos")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager", "Viewer"));

            group.MapGet("/{id}", async (int id, IContractService contractService) =>
            {
                var contract = await contractService.GetByIdAsync(id);
                return Results.Ok(contract);
            }).WithDescription("Retorna contrato pelo ID")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager", "Viewer"));

            group.MapGet("/status/{status}", async (EContractStatus status, IContractService contractService) =>
            {
                var contracts = await contractService.GetByStatusAsync(status);
                return Results.Ok(contracts);
            }).WithDescription("Retorna contratos pelo status")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager", "Viewer"));

            group.MapGet("/company/{companyId}", async (int companyId, IContractService contractService) =>
            {
                var contracts = await contractService.GetByCompanyAsync(companyId);
                return Results.Ok(contracts);
            }).WithDescription("Retorna todos os contratos de uma empresa")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager", "Viewer"));

            group.MapGet("/contact/{contactId}", async (int contactId, IContractService contractService) =>
            {
                var contracts = await contractService.GetByContactAsync(contactId);
                return Results.Ok(contracts);
            }).WithDescription("Retorna todos os contratos de um contato")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager", "Viewer"));

            group.MapGet("/expired", async (IContractService contractService) =>
            {
                var contracts = await contractService.GetExpiredAsync();
                return Results.Ok(contracts);
            }).WithDescription("Retorna contratos ativos ou suspensos com prazo vencido")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager", "Viewer"));

            group.MapPost("/", async (ContractRequestDTO contract, IContractService contractService) =>
            {
                await contractService.CreateAsync(contract);
                return Results.Created($"/contracts", contract);
            }).WithDescription("Cria novo contrato (status inicial: Draft)")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager"));

            group.MapPut("/{id}", async (int id, ContractRequestDTO contract, IContractService contractService) =>
            {
                await contractService.UpdateAsync(id, contract);
                return Results.Ok(contract);
            }).WithDescription("Atualiza contrato existente (não altera status)")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager"));

            group.MapPatch("/{id}/status", async (int id, UpdateContractStatusRequestDTO request, IContractService contractService) =>
            {
                await contractService.UpdateStatusAsync(id, request);
                return Results.Ok();
            }).WithDescription("Transiciona o status do contrato conforme fluxo permitido")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager"));

            group.MapDelete("/{id}", async (int id, IContractService contractService) =>
            {
                await contractService.DeleteAsync(id);
                return Results.NoContent();
            }).WithDescription("Deleta contrato (somente Draft ou Cancelled)")
            .RequireAuthorization(policy => policy.RequireRole("Admin"));
        }
    }
}
