

namespace Infrastructure
{
    public class Shipping_MethodRepository : GenericRepository<Shipping_Methods, int>, IShipping_MethodRepository
    {
        public Shipping_MethodRepository(AsusDbContext _asusDbContext) : base(_asusDbContext)
        {
        }
        public async Task<bool> ExistsAsync(string name)
        {
            return await dbset.AnyAsync(c => c.Method_Name == name);
        }

    }
}
