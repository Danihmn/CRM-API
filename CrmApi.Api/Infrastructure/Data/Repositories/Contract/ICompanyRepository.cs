namespace CrmApi.Api.Infrastructure.Data.Repositories.Contract
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company?> GetByCNPJAsync (string cnpj);
        Task<IEnumerable<Company>> GetBySegmentAsync (string segment);
    }
}
