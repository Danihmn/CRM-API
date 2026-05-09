namespace CrmApi.Api.Infrastructure.Data.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure (EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.Role).HasConversion<string>();
        }
    }
}
