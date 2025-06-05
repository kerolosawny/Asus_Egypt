
namespace DTO.Products
{
    public record AddSpecsDTO
    {
        public int Id { get; set; }
        public List<ProductSpecificationDTO> ProductSpecifications { get; set; } = new List<ProductSpecificationDTO>(); // List of specifications for the product

    }
}
