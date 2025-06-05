
namespace DTO.Products
{
    public class CreateProductDTO
    {
        public int? CreatedBy { get; set; }

        [Required(ErrorMessage ="Please enter product name")]
        [MaxLength(100)]
        [MinLength(3)]
        [Remote(action: "IsProductNameAvailable", controller: "Product", ErrorMessage = "The Product name already exists.")]

        public required string Name { get; set; }

        [Required(ErrorMessage = "Please enter product name")]
        [MaxLength(100)]
        [MinLength(3)]
        [Remote(action: "IsProductNameENAvailable", controller: "Product", ErrorMessage = "The Product name already exists.")]

        public required string Name_EN { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(500)]
        public string? Description_EN { get; set; }

        [Required(ErrorMessage = "Please enter product Price")]
        [Range(0.01, 200000)] 
        public decimal Price { get; set; }

        [Required (ErrorMessage = "Please enter numbers of unites in stock")]
        [Range(0, int.MaxValue)] 
        public int Unit_Instock { get; set; }


        public string? image_url { get; set; }

        [Required(ErrorMessage = "Please select a sub category")]
        public int SubCategoryId { get; set; }

        public ICollection<int>? Discounts { get; set; }
        public int? Code { get; set; }
        [Required(ErrorMessage = "Please enter product Image")]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg", ".svg", ".webp" })]

        public IFormFile? ImageData { get; set; }
        //public ICollection<ProductSpecificationDTO> ProductSpecifications { get; set; } = new List<ProductSpecificationDTO>();

        //public CreateProductDTO()
        //{

        //}
        //public CreateProductDTO(Model.Product prd)
        //{
        //    Name = prd.Name;
        //}
    }
}
