namespace CrmApi.Api.Infrastructure.Data.Repositories.Contract
{
    public interface IContactRepository : IRepository<ContactEntity>
    {
        Task<ContactEntity?> GetByEmailAsync (string email);
        Task<IEnumerable<ContactEntity>> GetByCompanyAsync (int companyId);
        Task<IEnumerable<ContactEntity>> GetByPositionAsync (string position);
    }
}
