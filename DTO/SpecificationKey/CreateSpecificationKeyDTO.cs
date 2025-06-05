 
namespace DTO.SpecificationKey
{
    public class CreateSpecificationKeyDTO
    {
        [MaxLength(100)]
        [Required]
        [Remote(action: "IsSpecNameAvailable", controller: "SpecificationKey", ErrorMessage = "The Specification name already exists.")]

        public required string Key { get; set; }

        public required string Key_Ar { get; set; }

        [Required]
        public List<int>? SubCategoryId { get; set; } 
    }

}
