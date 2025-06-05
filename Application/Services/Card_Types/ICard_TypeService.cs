
namespace Application.Services.Card_Types
{
    public interface ICard_TypeService 
    {
        Task<ResultView<CreateCard_TypeDTO>> CreateAsync(CreateCard_TypeDTO entity);
        Task<ResultView<UpdateCard_TypeDTO>> UpdateAsync(UpdateCard_TypeDTO entity);
        Task<bool> DeleteAsync(int id);
        Task<List<Card_TypeDTO>> GetAllAsync();
        Task<Card_TypeDTO> GetByIdAsync(int id);
        Task<bool> NameExistsAsync(string name);
    }
}
