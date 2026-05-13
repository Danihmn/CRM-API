namespace CrmApi.Api.Infrastructure.Services.Implementation
{
    public class AuthService
        (AppDbContext dbContext, IPasswordHasherService passwordHasherService, ITokenService tokenService) : IAuthService
    {
        public async Task<TokenResponseDTO> RegisterAsync (RegisterRequestDTO request)
        {
            var user = new UserEntity()
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = passwordHasherService.Hash(request.Password),
                Role = request.Role,
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return tokenService.GenerateToken(user);
        }

        public async Task<TokenResponseDTO> LoginAsync (LoginRequestDTO request)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Email == request.Email)
                ?? throw new UnauthorizedAccessException("Credenciais inválidas.");

            if (!passwordHasherService.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Credenciais inválidas.");

            return tokenService.GenerateToken(user);
        }
    }
}
