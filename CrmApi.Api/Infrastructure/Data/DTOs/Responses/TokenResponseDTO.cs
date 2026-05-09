namespace CrmApi.Api.Infrastructure.Data.DTOs.Responses
{
    public record TokenResponseDTO (string Token, DateTime ExpiresAt);
}
