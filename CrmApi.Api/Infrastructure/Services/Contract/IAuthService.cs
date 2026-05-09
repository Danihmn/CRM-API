namespace CrmApi.Api.Infrastructure.Services.Contract
{
    public interface IAuthService
    {
        Task<TokenResponseDTO> RegisterAsync (RegisterRequestDTO request);
        Task<TokenResponseDTO> LoginAsync (LoginRequestDTO request);
    }
}
