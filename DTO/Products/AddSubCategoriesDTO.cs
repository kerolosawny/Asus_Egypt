
namespace DTO.Products
{
    public record AddSubCategoriesDTO
    {
        public int ProductId { get; set; } 
        public ICollection<int> SubCategoryIds { get; set; } = new List<int>();
    }
}
