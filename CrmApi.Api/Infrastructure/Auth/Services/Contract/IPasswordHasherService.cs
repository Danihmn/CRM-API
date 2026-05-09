namespace CrmApi.Api.Infrastructure.Auth.Services.Contract
{
    public interface IPasswordHasherService
    {
        public string Hash (string password);
        public bool Verify (string password, string hashedPassword);
    }
}
