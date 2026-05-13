namespace CrmApi.Api.Domain.Entities
{
    public class CompanyEntity : Base
    {
        [MaxLength(150)]
        public required string CorporateName { get; set; }

        [MaxLength(14)]
        public required string CNPJ { get; set; }

        [MaxLength(100)]
        public required string Segment { get; set; }

        [MaxLength(200)]
        public string? WebSite { get; set; }
    }
}