

namespace DTO.Category
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Name_EN { get; set; } = string.Empty;
        public int? ParentCategoryID { get; set; }
        public ICollection<CategoryDTO>? ChildCategories { get; set; }
        public string? ImageUrl { get; set; }

    }
}
