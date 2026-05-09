namespace CrmApi.Api.Domain.Entities
{
    public class User : Base
    {
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(100)]
        public required string Email { get; set; }

        [MaxLength(255)]
        public required string PasswordHash { get; set; }

        [Required]
        public required EUserRole Role { get; set; }
    }
}
