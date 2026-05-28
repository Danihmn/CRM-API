namespace CrmApi.Api.Infrastructure.Data.DTOs.Responses
{
    public record ContractResponseDTO (
        int Id,
        string Title,
        decimal Value,
        string Status,
        DateTime StartDate,
        DateTime? EndDate,
        int ContactId,
        int CompanyId,
        DateTime CreatedAt);
}
