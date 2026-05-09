namespace CrmApi.Api.Infrastructure.Auth.Services.Implementation
{
    public class PasswordHasherService : IPasswordHasherService
    {
        private readonly PasswordHasher<object> _hasher = new();

        public string Hash (string password) =>
            _hasher.HashPassword(null!, password);

        public bool Verify (string password, string hashedPassword)
        {
            var result = _hasher.VerifyHashedPassword(null!, hashedPassword, password);
            return result != PasswordVerificationResult.Failed;
        }
    }
}
