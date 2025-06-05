 
namespace DTO.Usere
{
    public record UserRole
    {
        public int Id { get; set; } 
        public List<IdentityRole<int>>? Roles { get; set; }
        public string? SelectedRoleName { get; set; }
    }
}
