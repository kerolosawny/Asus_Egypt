

namespace Application.Contracts
{
    public interface IUserRepository :IEntityCount
    {
        public Task<User> GetOneAsync(string name);
        public  Task<User> GetByIDAsync(int Id);
        public Task<IQueryable<User>> GetSortedFilterAsync<TKey>(Expression<Func<User, TKey>> orderBy, Expression<Func<User, bool>> searchPredicate = null, bool ascending = true);
        public Task<IQueryable<User>> GetAllAsync();
        public Task<IQueryable<User>> GetAllAsync(int pageNumber = 1, int pageSize = 15);

    }
}
