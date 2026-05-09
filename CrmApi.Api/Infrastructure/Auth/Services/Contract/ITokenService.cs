namespace CrmApi.Api.Infrastructure.Auth.Services.Contract
{
    public interface ITokenService
    {
        TokenResponseDTO GenerateToken (User user);
    }
}
