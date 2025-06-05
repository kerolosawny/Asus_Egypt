

namespace DTO.Category
{
    public class CreateCategoryDTO
    {
        [Required]
        [MaxLength(100)]
        [Remote(action: "IsCategoryNameAvailable", controller: "Category", ErrorMessage = "The category name already exists.")]
        public required string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [Remote(action: "IsCategoryNameENAvailable", controller: "Category", ErrorMessage = "The category name already exists.")]

        public required string Name_EN { get; set; }
        public string? ImageUrl { get; set; }
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg", ".svg" })]

        public IFormFile? ImageData { get; set; }
        //  [Required]
        // [Range(300, 600)] 
        // public int Code { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
