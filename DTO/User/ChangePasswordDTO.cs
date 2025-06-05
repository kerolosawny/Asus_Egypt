
namespace DTO.Usere
{
    public record ChangePasswordDTO
    {
        public required string Email { get; set; } 
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
