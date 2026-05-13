namespace CrmApi.Api.Infrastructure.Data.Database.Configurations
{
    public class ContractConfiguration : IEntityTypeConfiguration<ContractEntity>
    {
        public void Configure (EntityTypeBuilder<ContractEntity> builder)
        {
            builder.Property(contract => contract.Status).HasConversion<string>();
        }
    }
}
