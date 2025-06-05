namespace Infrastructure
{
    public class GloableGenericRepository<TEntity, TId> : IGloableGenericRepository<TEntity, TId> where TEntity :class
    {
        protected readonly AsusDbContext asusDbContext;

        protected readonly DbSet<TEntity> dbset;

        public GloableGenericRepository(AsusDbContext _asusDbContext)
        {
            asusDbContext = _asusDbContext;
            dbset = asusDbContext.Set<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            return (await dbset.AddAsync(entity)).Entity;
        }


        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            dbset.Update(entity);
            await asusDbContext.SaveChangesAsync();
            return entity;
        }

        //hard delete
        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            var result = Task.FromResult(asusDbContext.Remove(entity).Entity);
            return  await result;
        }
      
        public virtual async ValueTask<TEntity> GetOneAsync(TId id)
        {
            var entity = await dbset.FindAsync(id);
            return entity;
        }

        public virtual Task<IQueryable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(dbset.Select(p => p));
        }

        public Task<int> SaveChangesAsync()
        {
            return asusDbContext.SaveChangesAsync();
        }
        public virtual async Task<int> EntityCount()
        {
            return (await dbset.CountAsync());
        }
        public virtual Task<IQueryable<TEntity>> GetAllAsync(int pageNumber = 1, int pageSize = 15)
        {
            if (pageNumber < 1) pageNumber = 1;
            return Task.FromResult(dbset.Skip((pageNumber - 1) * pageSize).Take(pageSize));
        }
        // method to sort as needed for last time
        public async Task<IQueryable<TEntity>> GetSortedFilterAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool ascending = true)
        {
            return await Task.FromResult(ascending ? dbset.OrderBy(orderBy) : dbset.OrderByDescending(orderBy));
        }
        public virtual async Task<IQueryable<TEntity>> GetSortedFilterAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy, Expression<Func<TEntity, bool>> searchPredicate = null, bool ascending = true)
        {
            var query = dbset.AsQueryable();

            if (searchPredicate != null)
            {
                query = query.Where(searchPredicate);
            }

            query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            return await Task.FromResult(query.Any() ? query : Enumerable.Empty<TEntity>().AsQueryable());
        }

    }
}
