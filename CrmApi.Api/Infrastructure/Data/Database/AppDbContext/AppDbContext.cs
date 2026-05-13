namespace CrmApi.Api.Infrastructure.Data.Database.AppDbContext
{
    public class AppDbContext (DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<CompanyEntity> Companies { get; set; }
        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<ContractEntity> Contracts { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
