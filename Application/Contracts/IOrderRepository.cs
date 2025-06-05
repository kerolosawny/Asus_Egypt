

namespace Application.Contracts
{
    public interface IOrderRepository : IGenericRepository<Order, int>
    {
        IQueryable<Order> GetByUserIdAsync(int userId);

        public  Task<int> MaxCode();
    }
}
