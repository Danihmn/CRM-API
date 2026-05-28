namespace CrmApi.Api.Infrastructure.Data.DTOs.Requests
{
    public record CompanyRequestDTO (string CorporateName, string CNPJ, string Segment, string? WebSite);
}
