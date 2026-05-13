namespace CrmApi.Api.Infrastructure.Data.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure (EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(user => user.Role).HasConversion<string>();
            builder.HasIndex(user => user.Email).IsUnique();
        }
    }
}
