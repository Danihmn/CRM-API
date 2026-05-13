namespace CrmApi.Api.Infrastructure.Data.DTOs.Requests
{
    public record ContractRequestDTO (
        string Title,
        decimal Value,
        DateTime StartDate,
        DateTime? EndDate,
        int ContactId,
        int CompanyId);
}
