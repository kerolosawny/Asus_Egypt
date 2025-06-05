
namespace Application.Services.Ordrs
{
    public interface IOrder_StateService 
    {
        Task<ResultView<Order_StateDTO>> CreateAsync(CreateOrder_StateDTO entity);
        Task<ResultView<Order_StateDTO>> UpdateAsync(UpdateOrder_StateDTO entity);
        Task<bool> DeleteAsync(int id);
        Task<List<Order_StateDTO>> GetAllAsync();
        Task<Order_StateDTO> GetByIdAsync(int id);
        Task<bool> NameExistsAsync(string name);
        Task<bool> NameENExistsAsync(string nameEN);
    }
}
