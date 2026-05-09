namespace CrmApi.Api.Infrastructure.Auth.Configurations
{
    public class TokenConfiguration
    {
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public int ExpirationInMinutes { get; set; }
        public required string SecretKey { get; set; }
    }
}
