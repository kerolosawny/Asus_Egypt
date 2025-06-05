namespace AsusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO createUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var result = await _userService.CreateAsync(createUserDto);

            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.Massage });
            }

            return Ok(new { message = "Registration successful", user = result.Entity });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var result = await _userService.LoginAsync(loginDto);

            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.Massage });
            }

            return Ok(new { token = result.Entity });
        }
        //[Authorize(Roles = "Admin,User")]

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var result = await _userService.ChangePasswordAsync(changePasswordDTO.Email, changePasswordDTO.OldPassword, changePasswordDTO.NewPassword);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Massage);
            }

            return Ok(result.Massage);
        }

        [HttpGet("get-user-by-email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var result = await _userService.GetByEmailAsync(email);

            if (!result.IsSuccess)
            {
                return NotFound(new { message = result.Massage });
            }

            return Ok(result.Entity);
        }
       // [Authorize(Roles = "Admin,User")]

        [HttpPatch("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDTO updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var result = await _userService.UpdateAsync(updateUserDto);

            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.Massage });
            }

            return Ok(new { message = "User updated successfully", user = result.Entity });
        }
    }
}
    





