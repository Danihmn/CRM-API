namespace CrmApi.Api.Infrastructure.Data.Repositories.Generic
{
    public class Repository<TEntity> (AppDbContext context) : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        public async Task<IEnumerable<TEntity>> GetAllAsync () => await _dbSet.ToListAsync();

        public async Task<TEntity?> GetByIdAsync (int id) => await _dbSet.FindAsync(id);

        public async Task CreateAsync (TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync (int id, TEntity entity)
        {
            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity is null) return;

            context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync (int id)
        {
            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity is null) return;

            _dbSet.Remove(existingEntity);
            await context.SaveChangesAsync();
        }
    }
}
