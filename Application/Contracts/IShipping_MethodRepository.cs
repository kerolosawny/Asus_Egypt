

namespace Application.Contracts
{
    public interface IShipping_MethodRepository : IGenericRepository<Shipping_Methods, int>
    {
        public Task<bool> ExistsAsync(string name);

    }
}
