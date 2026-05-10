namespace CrmApi.Api.Infrastructure.Auth.Services.Implementation
{
    public class TokenService (TokenConfiguration configuration) : ITokenService
    {
        public TokenResponseDTO GenerateToken (User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresAt = DateTime.UtcNow.AddMinutes(configuration.ExpirationInMinutes);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: configuration.Issuer,
                audience: configuration.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            return new TokenResponseDTO(user.Name, user.Email, new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
        }
    }
}
