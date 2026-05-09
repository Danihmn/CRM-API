namespace CrmApi.Api.Infrastructure.Data.Database.Configurations
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure (EntityTypeBuilder<Contract> builder)
        {
            builder.Property(contract => contract.Status).HasConversion<string>();
        }
    }
}
