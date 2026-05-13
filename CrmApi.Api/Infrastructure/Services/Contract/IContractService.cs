namespace CrmApi.Api.Infrastructure.Services.Contract
{
    public interface IContractService
    {
        Task<IEnumerable<ContractResponseDTO>> GetAllAsync ();
        Task<ContractResponseDTO> GetByIdAsync (int id);
        Task<IEnumerable<ContractResponseDTO>> GetByStatusAsync (EContractStatus status);
        Task<IEnumerable<ContractResponseDTO>> GetByCompanyAsync (int companyId);
        Task<IEnumerable<ContractResponseDTO>> GetByContactAsync (int contactId);
        Task<IEnumerable<ContractResponseDTO>> GetExpiredAsync ();
        Task CreateAsync (ContractRequestDTO request);
        Task UpdateAsync (int id, ContractRequestDTO request);
        Task UpdateStatusAsync (int id, UpdateContractStatusRequestDTO request);
        Task DeleteAsync (int id);
    }
}
