

namespace Application.Contracts
{
    public interface ICard_TypeRepository : IGenericRepository<Card_Type, int>
    {
        public Task<bool> ExistsAsync(string name);

    }
}
