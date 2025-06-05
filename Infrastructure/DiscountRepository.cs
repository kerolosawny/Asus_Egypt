
namespace Infrastructure
{
    public class DiscountRepository : GenericRepository<Discount, int>, IDiscountRepository
    {
        public DiscountRepository(AsusDbContext _asusDbContext) : base(_asusDbContext)
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
