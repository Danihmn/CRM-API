namespace CrmApi.Api.Api.Endpoints
{
    public static class ContactEndpoints
    {
        public static void MapContactEndpoints (this WebApplication app)
        {
            var group = app.MapGroup("/contacts").WithTags("Contacts");

            group.MapGet("/", async (IContactService contactService) =>
            {
                var contacts = await contactService.GetAllAsync();
                return Results.Ok(contacts);
            }).WithDescription("Retorna todos os contatos")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager", "Viewer"));

            group.MapGet("/{id}", async (int id, IContactService contactService) =>
            {
                var contact = await contactService.GetByIdAsync(id);
                return Results.Ok(contact);
            }).WithDescription("Retorna contato pelo ID")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager", "Viewer"));

            group.MapGet("/email/{email}", async (string email, IContactService contactService) =>
            {
                var contact = await contactService.GetByEmailAsync(email);
                return Results.Ok(contact);
            }).WithDescription("Retorna contato pelo e-mail")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager", "Viewer"));

            group.MapGet("/company/{companyId}", async (int companyId, IContactService contactService) =>
            {
                var contacts = await contactService.GetByCompanyAsync(companyId);
                return Results.Ok(contacts);
            }).WithDescription("Retorna todos os contatos de uma empresa")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager", "Viewer"));

            group.MapGet("/position/{position}", async (string position, IContactService contactService) =>
            {
                var contacts = await contactService.GetByPositionAsync(position);
                return Results.Ok(contacts);
            }).WithDescription("Retorna contatos pelo cargo/função")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager", "Viewer"));

            group.MapPost("/", async (ContactRequestDTO contact, IContactService contactService) =>
            {
                await contactService.CreateAsync(contact);
                return Results.Created($"/contacts/email/{contact.Email}", contact);
            }).WithDescription("Cria novo contato")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager"));

            group.MapPut("/{id}", async (int id, ContactRequestDTO contact, IContactService contactService) =>
            {
                await contactService.UpdateAsync(id, contact);
                return Results.Ok(contact);
            }).WithDescription("Atualiza contato existente")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager"));

            group.MapDelete("/{id}", async (int id, IContactService contactService) =>
            {
                await contactService.DeleteAsync(id);
                return Results.NoContent();
            }).WithDescription("Deleta contato existente")
            .RequireAuthorization(policy => policy.RequireRole("Admin"));
        }
    }
}
