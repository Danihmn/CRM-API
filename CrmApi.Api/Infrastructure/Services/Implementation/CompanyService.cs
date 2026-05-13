namespace CrmApi.Api.Infrastructure.Services.Implementation
{
    public class CompanyService (ICompanyRepository repository) : ICompanyService
    {
        public async Task<IEnumerable<CompanyResponseDTO>> GetAllAsync ()
        {
            var result = await repository.GetAllAsync();
            return result.Adapt<IEnumerable<CompanyResponseDTO>>();
        }

        public async Task<IEnumerable<CompanyResponseDTO>> GetBySegmentAsync (string segment)
        {
            var result = await repository.GetBySegmentAsync(segment);
            return result.Adapt<IEnumerable<CompanyResponseDTO>>();
        }

        public async Task<CompanyResponseDTO> GetByIdAsync (int id)
        {
            var result = await repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Company with id {id} not found.");

            return result.Adapt<CompanyResponseDTO>();
        }

        public async Task<CompanyResponseDTO> GetByCNPJAsync (string cnpj)
        {
            var result = await repository.GetByCNPJAsync(cnpj)
                ?? throw new NotFoundException($"Company with CNPJ {cnpj} not found.");

            return result.Adapt<CompanyResponseDTO>();
        }

        public async Task CreateAsync (CompanyRequestDTO request)
        {
            var existingCompany = await repository.GetByCNPJAsync(request.CNPJ);
            if (existingCompany != null)
                throw new InvalidOperationException($"Company with CNPJ {request.CNPJ} already exists.");

            var newCompany = new CompanyEntity
            {
                CorporateName = request.CorporateName,
                CNPJ = request.CNPJ,
                Segment = request.Segment,
                WebSite = request.WebSite,
                CreatedAt = DateTime.UtcNow
            };

            await repository.CreateAsync(newCompany);
        }

        public async Task UpdateAsync (int id, CompanyRequestDTO request)
        {
            var existingCompany = await repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Company with id {id} not found.");
            var newCompany = new CompanyEntity
            {
                CorporateName = request.CorporateName,
                CNPJ = request.CNPJ,
                Segment = request.Segment,
                WebSite = request.WebSite,
                CreatedAt = existingCompany.CreatedAt,
            };

            await repository.UpdateAsync(id, newCompany);
        }

        public async Task DeleteAsync (int id)
        {
            _ = await repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Company with id {id} not found.");

            await repository.DeleteAsync(id);
        }
    }
}
