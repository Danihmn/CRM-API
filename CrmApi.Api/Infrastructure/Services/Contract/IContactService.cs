namespace CrmApi.Api.Infrastructure.Services.Contract
{
    public interface IContactService
    {
        Task<IEnumerable<ContactResponseDTO>> GetAllAsync ();
        Task<ContactResponseDTO> GetByIdAsync (int id);
        Task<ContactResponseDTO> GetByEmailAsync (string email);
        Task<IEnumerable<ContactResponseDTO>> GetByCompanyAsync (int companyId);
        Task<IEnumerable<ContactResponseDTO>> GetByPositionAsync (string position);
        Task CreateAsync (ContactRequestDTO request);
        Task UpdateAsync (int id, ContactRequestDTO request);
        Task DeleteAsync (int id);
    }
}
