
namespace DTO.Category
{
    public class UpdateCategoryDTO
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
       // [Remote(action: "IsCategoryNameAvailable", controller: "Category", ErrorMessage = "The category name already exists.")]

        public string? Name { get; set; }

        [MaxLength(100)]
        [Required]
       // [Remote(action: "IsCategoryNameENAvailable", controller: "Category", ErrorMessage = "The category name already exists.")]

        public string? Name_EN { get; set; }
        public string? ImageUrl { get; set; }
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg", ".svg" })]

        public IFormFile? ImageData { get; set; }
        //[Range(300, 600)]
        //public int? Code { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
