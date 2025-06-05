
namespace Infrastructure
{
    public class GenericRepository<TEntity, TId> : GloableGenericRepository<TEntity, TId>, IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        
        public GenericRepository(AsusDbContext _asusDbContext):base(_asusDbContext) 
        {
            
        }
        //soft delete
        public async Task<TEntity> SoftDeleteAsync(TEntity entity)
        {
            entity.IsDeleted = true;
            dbset.Update(entity);
            await asusDbContext.SaveChangesAsync();
            return entity;
        }

        public override async ValueTask<TEntity> GetOneAsync(TId id)
        {
            var entity = await dbset.FindAsync(id);
            return entity != null && !entity.IsDeleted ? entity : null;
        }

        public override  Task<IQueryable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(dbset.OrderByDescending(p => p.Created_at).Where(entity => !entity.IsDeleted));
        }

      
        public override  async Task<int> EntityCount()
        {
            return (await dbset.CountAsync(p=>!p.IsDeleted));
        }
        public override Task<IQueryable<TEntity>> GetAllAsync(int pageNumber = 1, int pageSize = 15)
        {
            if (pageNumber < 1) pageNumber = 1;
            return Task.FromResult(dbset.Where(entity => !entity.IsDeleted).OrderByDescending(p => p.Created_at).Skip((pageNumber - 1) * pageSize).Take(pageSize));
        }
     
        public override async  Task<IQueryable<TEntity>> GetSortedFilterAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy, Expression<Func<TEntity, bool>> searchPredicate = null, bool ascending = true)
        {
            var query = dbset.AsQueryable();
            query=query.Where(p=>!p.IsDeleted);
            if (searchPredicate != null)
            {
                query = query.Where(searchPredicate);
            }
            query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            return await Task.FromResult(query.Any() ? query : Enumerable.Empty<TEntity>().AsQueryable());
        }
     
      
    }
}
