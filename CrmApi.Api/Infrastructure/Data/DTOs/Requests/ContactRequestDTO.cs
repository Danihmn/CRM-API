namespace CrmApi.Api.Infrastructure.Data.DTOs.Requests
{
    public record ContactRequestDTO (string Name, string Email, string Phone, string Position, int CompanyId);
}
