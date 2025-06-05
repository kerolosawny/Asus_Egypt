
namespace Infrastructure
{
    public class Order_ItemsRepository : GloableGenericRepository<Order_Items, int>, IOrder_ItemsRepository
    {
        public Order_ItemsRepository(AsusDbContext _asusDbContext) : base(_asusDbContext)
        {
        }
        public override   Task<IQueryable<Order_Items>> GetAllAsync()
        {
            return Task.FromResult(dbset.
                Include(p => p.Orders)
                .Include(p => p.Orders).Where(p=>p.Quantity>0)
                );
        }
    }
}
