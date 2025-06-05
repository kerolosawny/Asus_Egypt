 
namespace AsusAPI.Controllers
{
    //[Authorize(Roles = "Admin,User")]

    [Route("api/[controller]")]
    [ApiController]
    public class SpecificationsController : ControllerBase
    {
        public readonly ISpecificationKeyService _specificationKeyService;
        public SpecificationsController(ISpecificationKeyService specificationKeyService)
        {
            _specificationKeyService = specificationKeyService; 
        }

        [HttpGet("{catId}")]
        public async Task<IActionResult> GetSpecificationsByCategory(int catId)
        {
            var specifications = await _specificationKeyService.GetAllSpecificationKeysByCategoryAsync(catId);

            if (specifications == null || specifications.Count == 0)
            {
                return NotFound(); 
            }

            return Ok(specifications); 
        }
    }
}
