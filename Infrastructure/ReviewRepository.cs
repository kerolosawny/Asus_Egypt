
namespace Infrastructure
{
    public class ReviewRepository : GenericRepository<Reviews, int> , IReviewsRepository
    {
        public ReviewRepository(AsusDbContext asusDbContext) : base(asusDbContext)
        {

        }
    }
}
