namespace Application.Contracts
{
   public interface IGloableGenericRepository<TEntity, TId> 
    {
        public Task<TEntity> CreateAsync(TEntity Entity);
        public Task<TEntity> UpdateAsync(TEntity Entity);

        public Task<TEntity> DeleteAsync(TEntity Entity);

        public Task<IQueryable<TEntity>> GetAllAsync();
        public Task<IQueryable<TEntity>> GetAllAsync(int pageNumber = 1, int pageSize = 10);


        public ValueTask<TEntity> GetOneAsync(TId id);
        public Task<int> SaveChangesAsync();
        public Task<IQueryable<TEntity>> GetSortedFilterAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool ascending = true);
        public Task<IQueryable<TEntity>> GetSortedFilterAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy, Expression<Func<TEntity, bool>> searchPredicate = null, bool ascending = true);

    }
}
