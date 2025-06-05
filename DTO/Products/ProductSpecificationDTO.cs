

namespace DTO.Products
{
    public class ProductSpecificationDTO
    {
        public int SpecificationKeyId { get; set; } 
        public string? Name { get; set; }
        [Required]
        public string? Value { get; set; }
        public string? Name_AR { get; set; }

    }

}
