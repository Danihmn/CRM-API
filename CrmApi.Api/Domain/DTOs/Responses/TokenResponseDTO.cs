namespace CrmApi.Api.Infrastructure.Data.DTOs.Responses
{
    public record TokenResponseDTO (string Name, string Email, string Token, DateTime ExpiresAt);
}
