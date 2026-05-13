namespace CrmApi.Api.Infrastructure.Data.Repositories.Implementation
{
    public class CompanyRepository (AppDbContext context) : Repository<Company>(context), ICompanyRepository
    {
        public async Task<Company?> GetByCNPJAsync (string cnpj)
            => await context.Companies.FirstOrDefaultAsync(document => document.CNPJ == cnpj);

        public async Task<IEnumerable<Company>> GetBySegmentAsync (string segment)
            => await context.Companies.Where(company => company.Segment == segment).ToListAsync();
    }
}