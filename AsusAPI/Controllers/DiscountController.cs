namespace AsusAPI.Controllers
{
   // [Authorize(Roles = "Admin,User")]

    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly DiscountService _discountService;

        public DiscountController(DiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiscountsAsync()
        {
            var discounts = await _discountService.GetAllAsync();
            var filteredDiscounts = discounts.Where(p => p.ImageUrl != null && p.IsActive == true).ToList();
            return Ok(filteredDiscounts);
        }



    }
}
