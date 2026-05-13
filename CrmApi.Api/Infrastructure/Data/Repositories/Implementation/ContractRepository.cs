namespace CrmApi.Api.Infrastructure.Data.Repositories.Implementation
{
    public class ContractRepository (AppDbContext context) : Repository<ContractEntity>(context), IContractRepository
    {
        public async Task<IEnumerable<ContractEntity>> GetByStatusAsync (EContractStatus status)
            => await context.Contracts.Where(contract => contract.Status == status).ToListAsync();

        public async Task<IEnumerable<ContractEntity>> GetByCompanyAsync (int companyId)
            => await context.Contracts.Where(contract => contract.CompanyId == companyId).ToListAsync();

        public async Task<IEnumerable<ContractEntity>> GetByContactAsync (int contactId)
            => await context.Contracts.Where(contract => contract.ContactId == contactId).ToListAsync();

        public async Task<IEnumerable<ContractEntity>> GetExpiredAsync ()
            => await context.Contracts
                .Where(contract => (contract.Status == EContractStatus.Active || contract.Status == EContractStatus.Suspended)
                         && contract.EndDate != null
                         && contract.EndDate < DateTime.UtcNow)
                .ToListAsync();

        public async Task UpdateStatusAsync (int id, EContractStatus newStatus)
        {
            var contract = await context.Contracts.FindAsync(id);
            if (contract is null) return;

            contract.Status = newStatus;
            await context.SaveChangesAsync();
        }
    }
}
