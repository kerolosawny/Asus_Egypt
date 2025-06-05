
namespace DTO.Usere
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "User Name is required.")]
        public required string UserName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+={}\[\]:;""'<>,.?/~`-])[A-Za-z\d!@#$%^&*()_+={}\[\]:;""'<>,.?/~`-]{8,}$",
              ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public required string Password { get; set; }
        [Required(ErrorMessage = "Please confirm your password.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public required string ConfirmPassword { get; set; }

        //[MaxLength(255)]
        public DateTime? BirthDate { get; set; }

        [MaxLength(255)]
        public string? City { get; set; }

        [MaxLength(255)]
        public string? Region { get; set; }
        public bool RememberMe { get; set; }
        [MaxLength(25)]
        public string? PostalCode { get; set; }

        [MaxLength(255)]
        public string? Country { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
