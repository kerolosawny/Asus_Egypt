

namespace Application.Contracts
{
    public interface IOrder_stateRepository : IGenericRepository<Order_State, int>
    {
        public Task<bool> ExistsAsync(string nameEN, bool isEnglish);
        public Task<bool> ExistsAsync(string name);


    }
}
