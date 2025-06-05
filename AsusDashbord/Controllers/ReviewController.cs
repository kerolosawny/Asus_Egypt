namespace AsusDashbord.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ReviewController : Controller
    {
        private readonly IReviewsService reviewsService;
        private readonly IproductService productService;
        private readonly IMapper mapper;

        public ReviewController(IReviewsService _reviewsService, IproductService _productService, IMapper _mapper)
        {
            reviewsService = _reviewsService;
            productService = _productService;
            mapper = _mapper;
        }

        public async Task<IActionResult> Index()
        {
            var Reviews = await reviewsService.GetAllAsync();

            var rev= mapper.Map<ProductReviewDTO>(Reviews);

            return View("GetAllReviews", rev);
        }


        // Get product and its reviews by Product Id
       // public async Task<IActionResult> ProductReviews(int productId)
        //{
        //    var product = await productService.GetByIdAsync(productId);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    var reviews = await reviewsService.GetByProductIdAsync(productId);
        //    var productReviewsDto = new ProductReviewDTO
        //    {
        //        ProductId = product.Id,
        //        ProductName = product.Name,
        //        Reviews = reviews
        //    };

        //    return PartialView("GetAllReviews", productReviewsDto); 
        //}

        //// Update review
        //[HttpPost]
        //public async Task<IActionResult> UpdateReview(UpdateReviewsDTO model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await reviewsService.UpdateAsync(model);
        //        return Json(new { success = true });
        //    }

        //    return PartialView("UpdateReview", model);
        //}

        //// Delete review
        //[HttpPost]
        //public async Task<IActionResult> DeleteReview(int reviewId)
        //{
        //    var result = await reviewsService.DeleteAsync(reviewId);
        //    if (result)
        //    {
        //        return Json(new { success = true });
        //    }

        //    return Json(new { success = false, message = "Delete failed." });
        //}
    }

}

