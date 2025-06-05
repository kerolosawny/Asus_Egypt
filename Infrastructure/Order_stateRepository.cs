

namespace Infrastructure
{
    public class Order_stateRepository : GloableGenericRepository<Order_State, int>, IOrder_stateRepository

    {
        public Order_stateRepository(AsusDbContext _asusDbContext) : base(_asusDbContext)
        {
        }
        public async Task<bool> ExistsAsync(string name)
        {
            return await dbset.AnyAsync(c => c.Name == name);
        }
        public async Task<bool> ExistsAsync(string nameEN, bool isEnglish)
        {
            return await dbset.AnyAsync(c => c.Name_EN == nameEN);
        }
    }
}
