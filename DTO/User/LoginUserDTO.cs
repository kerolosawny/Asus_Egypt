

namespace DTO.Usere
{
    public record LoginUserDTO
    {
        [Required]
        [MaxLength(100)]
        public required string Username { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
