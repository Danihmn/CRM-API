namespace CrmApi.Api.Infrastructure.Data.Repositories.Contract
{
    public interface IContractRepository : IRepository<ContractEntity>
    {
        Task<IEnumerable<ContractEntity>> GetByStatusAsync (EContractStatus status);
        Task<IEnumerable<ContractEntity>> GetByCompanyAsync (int companyId);
        Task<IEnumerable<ContractEntity>> GetByContactAsync (int contactId);
        Task<IEnumerable<ContractEntity>> GetExpiredAsync ();
        Task UpdateStatusAsync (int id, EContractStatus newStatus);
    }
}
