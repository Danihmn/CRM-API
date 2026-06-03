namespace CrmApi.Api.Infrastructure.Data.Repositories.Implementation
{
    public class ContactRepository (AppDbContext context) : Repository<ContactEntity>(context), IContactRepository
    {
        public async Task<ContactEntity?> GetByEmailAsync (string email)
            => await context.Contacts
            .AsNoTracking()
            .FirstOrDefaultAsync(contact => contact.Email == email);

        public async Task<IEnumerable<ContactEntity>> GetByCompanyAsync (int companyId)
            => await context.Contacts
            .AsNoTracking()
            .Where(contact => contact.CompanyId == companyId)
            .ToListAsync();

        public async Task<IEnumerable<ContactEntity>> GetByPositionAsync (string position)
            => await context.Contacts
            .AsNoTracking()
            .Where(contact => contact.Position == position)
            .ToListAsync();
    }
}
