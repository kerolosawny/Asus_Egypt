
namespace DTO.Usere
{
    public class UpdateUserDTO
    {
       
        public int Id { get; set; }

         public string UserName { get; set; }=string.Empty;
         [EmailAddress(ErrorMessage = "Invalid email format.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [PasswordPropertyText]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+={}\[\]:;""'<>,.?/~`-])[A-Za-z\d!@#$%^&*()_+={}\[\]:;""'<>,.?/~`-]{8,}$",
              ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string? Password { get; set; }
        [MaxLength(255)]
        public  string BirthDate { get; set; }= string.Empty;
        [MaxLength(255)]
        public  string City { get; set; }= string.Empty ;
        [MaxLength(255)]

        public  string? Region { get; set; }
        [MaxLength(25)]

        public string? PostalCode { get; set; }
        [MaxLength(255)]

        public  string? Country { get; set; }
        [MaxLength(15)]
        public  string? PhoneNumber { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

    }
}
