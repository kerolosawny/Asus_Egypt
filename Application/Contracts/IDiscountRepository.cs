
namespace Application.Contracts
{
    public interface IDiscountRepository : IGenericRepository<Discount, int>
    {
        Task<bool> ExistsAsync(string name);
        Task<bool> ExistsAsync(string nameEN, bool isEnglish);


    }
}
