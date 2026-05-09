namespace CrmApi.Api.Infrastructure.Auth.Services.Implementation
{
    public class TokenService (TokenConfiguration config) : ITokenService
    {
        public TokenResponseDTO GenerateToken (User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresAt = DateTime.UtcNow.AddMinutes(config.ExpirationInMinutes);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: config.Issuer,
                audience: config.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            return new(new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
        }
    }
}
