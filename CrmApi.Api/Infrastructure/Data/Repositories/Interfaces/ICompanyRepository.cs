namespace CrmApi.Api.Infrastructure.Data.Repositories.Contract
{
    public interface ICompanyRepository : IRepository<CompanyEntity>
    {
        Task<CompanyEntity?> GetByCNPJAsync (string cnpj);
        Task<IEnumerable<CompanyEntity>> GetBySegmentAsync (string segment);
    }
}
