
namespace Infrastructure
{
    public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
    {

        public OrderRepository(AsusDbContext asusDbContext) : base(asusDbContext)
        {

        }

        public override Task<IQueryable<Order>> GetAllAsync(int pageNumber = 1, int pageSize = 15)
        {
            return Task.FromResult(dbset.OrderByDescending(p => p.Created_at)
               .Include(p => p.User)
                .Include(p => p.payment_Methods)
               .Include(p => p.shipping_Methods)
               .Include(p => p.order_State)
               .Include(p => p.Order_Items)!
               .ThenInclude(p => p.Products).
               Where(entity => !entity.IsDeleted).
               Skip((pageNumber - 1) * pageSize).
               Take(pageSize)
               );
        }

        public override async ValueTask<Order> GetOneAsync(int id)
        {
            var entity = dbset.AsQueryable();
            var order = await entity.Where(p => p.Id == id && !p.IsDeleted)
                .Include(p => p.Order_Items)!
               .ThenInclude(p => p.Products)
                 .Include(p => p.User)
                 .Include(p => p.order_State)
                 .Include(p => p.shipping_Methods)
                 .Include(p => p.payment_Methods)
                 .FirstOrDefaultAsync();
            return order;
        }
        public IQueryable<Order> GetByUserIdAsync(int userId)
        {
            return dbset.Where(order => order.UserId == userId);
            //    .Include(order => order.User)
            //.Include(order => order.Order_Items)
            //.Include(order => order.order_State)
            //.Include(order => order);
        }

        public override async Task<IQueryable<Order>> GetSortedFilterAsync<TKey>(Expression<Func<Order, TKey>> orderBy, Expression<Func<Order, bool>> searchPredicate = null, bool ascending = true)
        {
            var query = dbset.AsQueryable();

            if (searchPredicate != null)
            {
                query = query.Where(searchPredicate);
            }

            query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            query=  query.Any() ?  query.Include(p => p.User)
                .Include(p => p.payment_Methods)
               .Include(p => p.shipping_Methods)
               .Include(p => p.order_State)
               .Include(p => p.Order_Items).
               Where(entity => !entity.IsDeleted)
               : Enumerable.Empty<Order>().AsQueryable();
            return await Task.FromResult( query);
        }
        public async Task<int> MaxCode()
        {
            var maxCode = await dbset.MaxAsync(p => p.Code);
            return maxCode ?? 999;
        }
    }

}
