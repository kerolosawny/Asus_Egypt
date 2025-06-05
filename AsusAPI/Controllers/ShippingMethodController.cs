 
namespace AsusAPI.Controllers
{
    //[Authorize(Roles = "Admin,User")]

    [Route("api/[controller]")]
    [ApiController]
    public class ShippingMethodController : ControllerBase
    {
        private readonly IShippingMethodService _shippingMethodService;

        public ShippingMethodController(IShippingMethodService shippingMethodService)
        {
            _shippingMethodService = shippingMethodService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ShippingMethodsDTO>>> GetAll()
        {
            try
            {
                var shippingMethods = await _shippingMethodService.GetAllAsync();
                return Ok(shippingMethods);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving shipping methods.");
            }
        }
    }
}
