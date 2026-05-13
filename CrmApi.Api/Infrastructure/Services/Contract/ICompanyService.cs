namespace CrmApi.Api.Infrastructure.Services.Contract
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyResponseDTO>> GetAllAsync ();
        Task<IEnumerable<CompanyResponseDTO>> GetBySegmentAsync (string segment);
        Task<CompanyResponseDTO> GetByIdAsync (int id);
        Task<CompanyResponseDTO> GetByCNPJAsync (string cnpj);
        Task CreateAsync (CompanyRequestDTO request);
        Task UpdateAsync (int id, CompanyRequestDTO request);
        Task DeleteAsync (int id);
    }
}
