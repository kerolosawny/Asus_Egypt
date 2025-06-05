 
namespace DTO.Usere
{
    public class LoginDTO
    {
         [Required(ErrorMessage = "User Name Or Email is required.")]
        public required string UserNameOrEmail { get; set; }
        [Required(ErrorMessage = "Password is required.")]
         public required string Password { get; set; }
        public bool RememberMe { get; set; }
        public string? Result { get; set; }
    }
}
