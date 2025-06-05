
namespace DTO.Usere
{
    public class CreateRoleDTO
    {
        [Required(ErrorMessage = "Role name is required.")]
        [MaxLength(256)]
        public string Name { get; set; }=string.Empty;

    }
}
