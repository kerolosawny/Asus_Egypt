

namespace Application.Services.Discounte
{
    public interface IDiscountService  
    {
        Task<ResultView<DiscountDTO>> CreateAsync(CreateDiscountDTO entity);
        Task<ResultView<DiscountDTO>> UpdateAsync(UpdateDiscountDTO entity);
        Task<bool> DeleteAsync(int id);
        Task<List<DiscountDTO>> GetAllAsync();
        Task<DiscountDTO> GetByIdAsync(int id);
        Task<bool> NameExistsAsync(string name);
        Task<bool> NameENExistsAsync(string nameEN);


    }
}
