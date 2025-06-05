
namespace Application.Services.Shipping_Methodes
{
    public interface IShippingMethodService
    {
        Task<ResultView<ShippingMethodsDTO>> CreateAsync(CreateShippingMethodsDTO entity);
        Task<ResultView<ShippingMethodsDTO>> UpdateAsync(UpdateShippingMethodsDTO entity);
        Task<bool> DeleteAsync(int id);
        Task<List<ShippingMethodsDTO>> GetAllAsync();
        Task<ShippingMethodsDTO> GetByIdAsync(int id);
        Task<bool> NameExistsAsync(string name);
    }

}
