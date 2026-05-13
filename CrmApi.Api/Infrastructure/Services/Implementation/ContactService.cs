namespace CrmApi.Api.Infrastructure.Services.Implementation
{
    public class ContactService (IContactRepository repository, ICompanyRepository companyRepository) : IContactService
    {
        public async Task<IEnumerable<ContactResponseDTO>> GetAllAsync ()
        {
            var result = await repository.GetAllAsync();
            return result.Adapt<IEnumerable<ContactResponseDTO>>();
        }

        public async Task<ContactResponseDTO> GetByIdAsync (int id)
        {
            var result = await repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Contact with id {id} not found.");

            return result.Adapt<ContactResponseDTO>();
        }

        public async Task<ContactResponseDTO> GetByEmailAsync (string email)
        {
            var result = await repository.GetByEmailAsync(email)
                ?? throw new NotFoundException($"Contact with email {email} not found.");

            return result.Adapt<ContactResponseDTO>();
        }

        public async Task<IEnumerable<ContactResponseDTO>> GetByCompanyAsync (int companyId)
        {
            var result = await repository.GetByCompanyAsync(companyId);
            return result.Adapt<IEnumerable<ContactResponseDTO>>();
        }

        public async Task<IEnumerable<ContactResponseDTO>> GetByPositionAsync (string position)
        {
            var result = await repository.GetByPositionAsync(position);
            return result.Adapt<IEnumerable<ContactResponseDTO>>();
        }

        public async Task CreateAsync (ContactRequestDTO request)
        {
            var existing = await repository.GetByEmailAsync(request.Email);
            if (existing is not null)
                throw new InvalidOperationException($"Contact with email {request.Email} already exists.");

            _ = await companyRepository.GetByIdAsync(request.CompanyId)
                ?? throw new NotFoundException($"Company with id {request.CompanyId} not found.");

            var contact = new ContactEntity
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Position = request.Position,
                CompanyId = request.CompanyId,
                CreatedAt = DateTime.UtcNow
            };

            await repository.CreateAsync(contact);
        }

        public async Task UpdateAsync (int id, ContactRequestDTO request)
        {
            var existing = await repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Contact with id {id} not found.");

            _ = await companyRepository.GetByIdAsync(request.CompanyId)
                ?? throw new NotFoundException($"Company with id {request.CompanyId} not found.");

            var updated = new ContactEntity
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Position = request.Position,
                CompanyId = request.CompanyId,
                CreatedAt = existing.CreatedAt
            };

            await repository.UpdateAsync(id, updated);
        }

        public async Task DeleteAsync (int id)
        {
            _ = await repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Contact with id {id} not found.");

            await repository.DeleteAsync(id);
        }
    }
}
