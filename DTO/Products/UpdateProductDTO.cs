
namespace DTO.Products
{
    public class UpdateProductDTO
    {
        public UpdateProductDTO()
        {
            
        }
        public UpdateProductDTO(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Name_EN = product.Name_EN;
            Description = product.description;
            Description_EN = product.description_EN;
            Price = product.price;
            Unit_Instock = product.Unit_Instock;
            Image = product.image_url;
            Is_Active=product.IsActive;
             Discounts = product.Discount!=null? product.Discount.Select(p => p.discount!=null? p.discount.Id:0).ToList():new List<int>();
            Code = product.Code;
            priceAfterDiscount= product.priceAfterDiscount;
            Updated_By = product.Updated_By;
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(3)]

        public string? Name { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(3)]

        public string? Name_EN { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(500)]
        public string? Description_EN { get; set; }

        [Required(ErrorMessage = "Please enter product Price")]
        [Range(0.01, 200000)]
        public decimal? Price { get; set; }
        public int? Code { get; set; }
        public decimal? priceAfterDiscount { get; set; }

        [Required(ErrorMessage = "Please enter numbers of unites in stock")] 
        [Range(0, int.MaxValue)]
        public int? Unit_Instock { get; set; }
        public bool Is_Active { get; set; }
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg", ".svg", ".webp" })]

        public IFormFile? ImageD { get; set; }
        public string? Image {  get; set; }
        public int? Updated_By { get; set; }

        public int CategoryId { get; set; }
        public int DiscountId { get; set; }
        public ICollection<int>? Discounts { get; set; }
        //public Dictionary<string, string>? ProductAttribute { get; set; }
        //public ICollection<ProductSpecificationDTO> ProductSpecifications { get; set; } = new List<ProductSpecificationDTO>();
    }
    public record UpdateProductPrice
    {
        public int ProductId { get; set; }
        public int quantity { get; set; }
        public bool IsSubtract { get; set; }=true;
    }
}
