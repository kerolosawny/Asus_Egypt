
namespace DTO.Products
{
    public record AddImagesDTO
    {
        public int Id { get; set; }
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg", ".svg", ".webp" })]

        public ICollection<IFormFile>? Images { get; set; }
        public List<string> ExistingImages { get; set; } = new(); 


    }
}
