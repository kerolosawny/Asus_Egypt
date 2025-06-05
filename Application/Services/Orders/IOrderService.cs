
namespace Application.Services.Ordrs
{
    public interface IOrderService
    {
        Task<ResultView<OrderDTO>> CreateAsync(CreateOrderDTO entity);
        Task<ResultView<OrderDTO>> UpdateAsync(UpdateOrderDTO entity);
        Task<bool> DeleteAsync(int id);
        Task<List<OrderDTO>> GetAllAsync();
        Task<OrderDTO> GetByIdAsync(int id);
        Task UpdateOrderState(int orderId, int orderStateId);
        void RemoveFromCache(string key);
        void RemoveFromCashMemoery(string key, OrderDTO item);
        List<OrderDTO> AddtoCashMemoery(string key, List<OrderDTO> items);
        T GetFromCache<T>(string key);
        void AddToCache(string key, object value, TimeSpan? absoluteExpiration = null);
        public Task<IQueryable<Order>> GetSortedFilterAsync<TKey>(Expression<Func<Order, TKey>> orderBy, Expression<Func<Order, bool>> searchPredicate = null, bool ascending = true);
        public Task<EntityPaginated<OrderDTO>> GetAllAsync(int PageNumber = 1, int Pagesize = 15);
        Task<List<OrderDTO>> GetByUserIdAsync(int userId);
        public void Intialize();
        public Task<IQueryable<Order_Items>> GetTopSellingProductsAsync();


    }
}
