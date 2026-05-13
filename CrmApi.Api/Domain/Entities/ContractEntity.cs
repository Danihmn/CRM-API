namespace CrmApi.Api.Domain.Entities
{
    public class ContractEntity : Base
    {
        [MaxLength(150)]
        public required string Title { get; set; }

        [Precision(18, 2)]
        public decimal Value { get; set; }

        public EContractStatus Status { get; set; } = EContractStatus.Draft;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int ContactId { get; set; }

        public int CompanyId { get; set; }
    }
}
