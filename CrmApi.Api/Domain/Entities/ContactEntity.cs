namespace CrmApi.Api.Domain.Entities
{
    public class ContactEntity : Base
    {
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(100)]
        public required string Email { get; set; }

        [MaxLength(100)]
        public required string Phone { get; set; }

        [MaxLength(100)]
        public required string Position { get; set; }

        public int CompanyId { get; set; }
    }
}
