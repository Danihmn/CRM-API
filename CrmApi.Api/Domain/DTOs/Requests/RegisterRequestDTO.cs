namespace CrmApi.Api.Infrastructure.Data.DTOs.Requests
{
    public record RegisterRequestDTO (string Name, string Email, string Password, EUserRole Role);
}
