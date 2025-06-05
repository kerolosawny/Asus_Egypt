
namespace DTO.SpecificationKey
{
    public class UpdateSpecificationKeyDTO
    {
        public int Id { get; set; }  

        [MaxLength(100)]
        [Required]
        public required string Key { get; set; }
        [MaxLength(100)]
        [Required]
        public required string Key_Ar { get; set; }

        [Required]
        public List<int>? SubCategoryId { get; set; } 
    }

}
