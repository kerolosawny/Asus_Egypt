

namespace Infrastructure
{
    public class Card_TypeRepository : GloableGenericRepository<Card_Type, int>, ICard_TypeRepository
    {
        public Card_TypeRepository(AsusDbContext _asusDbContext) : base(_asusDbContext)
        {
        }
        public async Task<bool> ExistsAsync(string name)
        {
            return await dbset.AnyAsync(c => c.Name == name);
        }

    }
}