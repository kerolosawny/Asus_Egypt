
namespace Application.Services.Reviewes
{
    public interface IReviewsService
    {
        Task<ResultView<ReviewsDTO>> CreateAsync(CreateReviewsDTO entity);
        Task<ResultView<ReviewsDTO>> UpdateAsync(UpdateReviewsDTO entity);
        Task<bool> DeleteAsync(int id);
        Task<List<ReviewsDTO>> GetAllAsync();
        Task<ReviewsDTO> GetByIdAsync(int id);
        Task<List<ReviewsDTO>> GetByProductIdAsync(int productId);
        Task<List<ReviewsDTO>> GetByUserIdAsync(int userId);
    }

}
