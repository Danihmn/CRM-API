namespace CrmApi.Api.Infrastructure.Services.Implementation
{
    public class ContractService
        (IContractRepository repository, ICompanyRepository companyRepository, IContactRepository contactRepository)
        : IContractService
    {
        // Define quais status transitions são permitidas para cada status atual
        private static readonly Dictionary<EContractStatus, EContractStatus[]> _allowedTransitions = new()
        {
            { EContractStatus.Draft, [EContractStatus.Active] },
            { EContractStatus.Active, [EContractStatus.Suspended, EContractStatus.Completed, EContractStatus.Cancelled] },
            { EContractStatus.Suspended, [EContractStatus.Active, EContractStatus.Cancelled] },
            { EContractStatus.Completed, [] },
            { EContractStatus.Cancelled, [] },
        };

        public async Task<IEnumerable<ContractResponseDTO>> GetAllAsync ()
        {
            var result = await repository.GetAllAsync();
            return result.Adapt<IEnumerable<ContractResponseDTO>>();
        }

        public async Task<ContractResponseDTO> GetByIdAsync (int id)
        {
            var result = await repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Contract with id {id} not found.");

            return result.Adapt<ContractResponseDTO>();
        }

        public async Task<IEnumerable<ContractResponseDTO>> GetByStatusAsync (EContractStatus status)
        {
            var result = await repository.GetByStatusAsync(status);
            return result.Adapt<IEnumerable<ContractResponseDTO>>();
        }

        public async Task<IEnumerable<ContractResponseDTO>> GetByCompanyAsync (int companyId)
        {
            var result = await repository.GetByCompanyAsync(companyId);
            return result.Adapt<IEnumerable<ContractResponseDTO>>();
        }

        public async Task<IEnumerable<ContractResponseDTO>> GetByContactAsync (int contactId)
        {
            var result = await repository.GetByContactAsync(contactId);
            return result.Adapt<IEnumerable<ContractResponseDTO>>();
        }

        public async Task<IEnumerable<ContractResponseDTO>> GetExpiredAsync ()
        {
            var result = await repository.GetExpiredAsync();
            return result.Adapt<IEnumerable<ContractResponseDTO>>();
        }

        public async Task CreateAsync (ContractRequestDTO request)
        {
            _ = await companyRepository.GetByIdAsync(request.CompanyId)
                ?? throw new NotFoundException($"Company with id {request.CompanyId} not found.");

            _ = await contactRepository.GetByIdAsync(request.ContactId)
                ?? throw new NotFoundException($"Contact with id {request.ContactId} not found.");

            var contract = new ContractEntity
            {
                Title = request.Title,
                Value = request.Value,
                Status = EContractStatus.Draft,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ContactId = request.ContactId,
                CompanyId = request.CompanyId,
                CreatedAt = DateTime.UtcNow
            };

            await repository.CreateAsync(contract);
        }

        public async Task UpdateAsync (int id, ContractRequestDTO request)
        {
            var existing = await repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Contract with id {id} not found.");

            _ = await companyRepository.GetByIdAsync(request.CompanyId)
                ?? throw new NotFoundException($"Company with id {request.CompanyId} not found.");

            _ = await contactRepository.GetByIdAsync(request.ContactId)
                ?? throw new NotFoundException($"Contact with id {request.ContactId} not found.");

            var updated = new ContractEntity
            {
                Title = request.Title,
                Value = request.Value,
                Status = existing.Status,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ContactId = request.ContactId,
                CompanyId = request.CompanyId,
                CreatedAt = existing.CreatedAt
            };

            await repository.UpdateAsync(id, updated);
        }

        public async Task UpdateStatusAsync (int id, UpdateContractStatusRequestDTO request)
        {
            var existing = await repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Contract with id {id} not found.");

            if (!_allowedTransitions[existing.Status].Contains(request.NewStatus))
                throw new InvalidOperationException(
                    $"Transition from {existing.Status} to {request.NewStatus} is not allowed.");

            await repository.UpdateStatusAsync(id, request.NewStatus);
        }

        public async Task DeleteAsync (int id)
        {
            var existing = await repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Contract with id {id} not found.");

            if (existing.Status is not EContractStatus.Draft and not EContractStatus.Cancelled)
                throw new InvalidOperationException(
                    $"Cannot delete a contract with status {existing.Status}. Only Draft or Cancelled contracts can be deleted.");

            await repository.DeleteAsync(id);
        }
    }
}
