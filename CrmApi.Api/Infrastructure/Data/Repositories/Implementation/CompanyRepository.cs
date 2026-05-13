namespace CrmApi.Api.Infrastructure.Data.Repositories.Implementation
{
    public class CompanyRepository (AppDbContext context) : Repository<CompanyEntity>(context), ICompanyRepository
    {
        public async Task<CompanyEntity?> GetByCNPJAsync (string cnpj)
            => await context.Companies.FirstOrDefaultAsync(document => document.CNPJ == cnpj);

        public async Task<IEnumerable<CompanyEntity>> GetBySegmentAsync (string segment)
            => await context.Companies.Where(company => company.Segment == segment).ToListAsync();
    }
}