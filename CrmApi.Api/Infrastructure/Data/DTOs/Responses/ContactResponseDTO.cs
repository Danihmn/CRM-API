namespace CrmApi.Api.Infrastructure.Data.DTOs.Responses
{
    public record ContactResponseDTO (
        int Id,
        string Name,
        string Email,
        string Phone,
        string Position,
        int CompanyId,
        DateTime CreatedAt);
}
