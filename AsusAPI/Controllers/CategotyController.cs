namespace AsusAPI.Controllers
{
    //[Authorize(Roles = "Admin,User")]

    [Route("api/[controller]")]
    [ApiController]
    public class CategotyController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategotyController(ICategoryService category)
        {
            _categoryService = category;
        }

        [HttpGet("subcategories")]
        public async Task<IActionResult> GetSubCategoriesByParentName([FromQuery] string parentCategoryName)
        {
            if (string.IsNullOrWhiteSpace(parentCategoryName))
            {
                return BadRequest("Parent category name is required.");
            }

            var subCategories = await _categoryService.GetSubCategoriesByCategoryNameAsync(parentCategoryName);

            if (subCategories == null || subCategories.Count == 0)
            {
                return NotFound("No subcategories found for the specified parent category.");
            }

            return Ok(subCategories);
        }
        [HttpGet("with-images")]
        public async Task<IActionResult> GetCategoriesWithImages()
        {
            var categories = (await _categoryService.GetAllAsync())
                .Where(a => a.ImageUrl != null)  
                .Take(4)  
                .ToList();  

            return Ok(categories);
        }

    }
}
