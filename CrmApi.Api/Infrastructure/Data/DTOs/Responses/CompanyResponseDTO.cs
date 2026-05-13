namespace CrmApi.Api.Infrastructure.Data.DTOs.Responses
{
    public record CompanyResponseDTO (int Id, string CorporateName, string CNPJ, string Segment, string? WebSite);
}
