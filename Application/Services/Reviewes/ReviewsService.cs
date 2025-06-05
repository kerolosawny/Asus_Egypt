
namespace Application.Services.Reviewes
{
    public class ReviewsService : IReviewsService
    {
        private readonly IReviewsRepository _reviewsRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ReviewsService(
            IReviewsRepository reviewsRepository,
            IProductRepository productRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _reviewsRepository = reviewsRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // Create Review
        public async Task<ResultView<ReviewsDTO>> CreateAsync(CreateReviewsDTO entity)
        {
            ResultView<ReviewsDTO> result = new();
            try
            {
                // Validate Product and User existence
                var product = await _productRepository.GetOneAsync(entity.ProductId);
                var user = await _userRepository.GetByIDAsync(entity.UserId);
                if (product == null || user == null)
                {
                    result = new ResultView<ReviewsDTO>
                    {
                         IsSuccess = false,
                        Massage = "Invalid Product or User"
                    };
                    return result;
                }

                // Map DTO to Reviews entity
                var review = _mapper.Map<Reviews>(entity);
                review.product = product;
                review.UserReviews = new List<User_Reviews> { new User_Reviews { user = user, review = review } };

                var createdReview = await _reviewsRepository.CreateAsync(review);
                await _reviewsRepository.SaveChangesAsync();

                // Map back to DTO and return
                var returnReview = _mapper.Map<ReviewsDTO>(createdReview);
                result = new ResultView<ReviewsDTO>
                {
                    Entity = returnReview,
                    IsSuccess = true,
                    Massage = "Review created successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<ReviewsDTO>
                {
                     IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message
                };
                return result;
            }
        }

        // Update Review
        public async Task<ResultView<ReviewsDTO>> UpdateAsync(UpdateReviewsDTO entity)
        {
            ResultView<ReviewsDTO> result = new();
            try
            {
                var review = await _reviewsRepository.GetOneAsync(entity.Id);
                if (review == null)
                {
                    result = new ResultView<ReviewsDTO>
                    {
                        IsSuccess = false,
                        Massage = "Review not found",
                     };
                    return result;
                }

                // Update review details
                _mapper.Map(entity, review);
                await _reviewsRepository.UpdateAsync(review);
                await _reviewsRepository.SaveChangesAsync();

                var updatedReviewDto = _mapper.Map<ReviewsDTO>(review);
                result = new ResultView<ReviewsDTO>
                {
                    Entity = updatedReviewDto,
                    IsSuccess = true,
                    Massage = "Review updated successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<ReviewsDTO>
                {
                    IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message,
                 };
                return result;
            }
        }

        // Delete Review
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var review = await _reviewsRepository.GetOneAsync(id);
                if (review == null) return false;

                await _reviewsRepository.DeleteAsync(review);
                await _reviewsRepository.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Get All Reviews
        public async Task<List<ReviewsDTO>> GetAllAsync()
        {
            var reviews = await _reviewsRepository.GetAllAsync();
            return _mapper.Map<List<ReviewsDTO>>(reviews);
        }

        // Get Review by ID
        public async Task<ReviewsDTO> GetByIdAsync(int id)
        {
            var review = await _reviewsRepository.GetOneAsync(id);
            return _mapper.Map<ReviewsDTO>(review);
        }

        // Get Reviews by Product ID
        public async Task<List<ReviewsDTO>> GetByProductIdAsync(int productId)
        {
            var reviews = await _reviewsRepository.GetAllAsync();
            var productReviews = reviews.Where(r => r.ProductId == productId).ToList();
            return _mapper.Map<List<ReviewsDTO>>(productReviews);
        }

        // Get Reviews by User ID
        public async Task<List<ReviewsDTO>> GetByUserIdAsync(int userId)
        {
            var reviews = await _reviewsRepository.GetAllAsync();
            var userReviews = reviews.Where(r => r.UserReviews!=null&& r.UserReviews.Any(ur => ur.UserId == userId)).ToList();
            return _mapper.Map<List<ReviewsDTO>>(userReviews);
        }
    }

}
