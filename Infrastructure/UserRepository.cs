

namespace Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly AsusDbContext asusDbContext;

        protected readonly DbSet<User> dbset;
        public UserRepository(AsusDbContext _asusDbContext)
        {
            asusDbContext = _asusDbContext;
            dbset = asusDbContext.Set<User>();
        }

        public async Task<IQueryable<User>> GetAllAsync()
        {
            return await Task.FromResult(dbset.Select(p => p));
        }
        public Task<IQueryable<User>> GetAllAsync(int pageNumber = 1, int pageSize = 15)
        {
            if (pageNumber <1) pageNumber = 1;
            return Task.FromResult(dbset.Skip((pageNumber - 1) * pageSize).Take(pageSize));
        }

        public async Task<IQueryable<User>> GetSortedFilterAsync<TKey>(Expression<Func<User, TKey>> orderBy, Expression<Func<User, bool>> searchPredicate = null, bool ascending = true)
        {
            var query = dbset.AsQueryable();

            if (searchPredicate != null)
            {
                query = query.Where(searchPredicate);
            }

            query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            return await Task.FromResult(query.Any() ? query : Enumerable.Empty<User>().AsQueryable());
        }

        public async Task<User> GetOneAsync(string Name)
        {
            return await dbset.FirstOrDefaultAsync(p => p.UserName == Name);
        }
        public async Task<User> GetByIDAsync(int Id)
        {
            return await dbset.FindAsync(Id);
        }
        public async Task<int> EntityCount()
        {
            return (await dbset.CountAsync());
        }
    }
}
