
namespace Model
{
    public class Product : BaseEntity<int>
    {

        public Product()
        {
            Images = new List<Product_Image>();
            Product_Category = new List<Product_Categoty>();
            Discount = new List<Product_Discount>();
            ProductSpecifications = new List<ProductSpecification>();
            Reviews = new List<Reviews>();
        }
        [MaxLength(100)]
        public required string Name { get; set; }
        [MaxLength(100)]
        public required string Name_EN { get; set; }
        [MaxLength(500)]
        public string? description { get; set; }
        [MaxLength(500)]
        public string? description_EN { get; set; }
        [MaxLength(300)]
        public required string image_url { get; set; }
        [Range(333, 100_000)]
        public int? Code { get; set; }
        [Column(TypeName = "money")]
        public decimal price { get; set; }
        [Column(TypeName = "money")]
        public decimal? priceAfterDiscount { get; set; }
        public int Unit_Instock { get; set; }
 
        public bool IsActive { get; set; } = false;

        public virtual ICollection<Product_Image>? Images { get; set; }

        public virtual ICollection<Product_Categoty>? Product_Category { get; set; }
        public virtual ICollection<Product_Discount>? Discount { get; set; }
        public virtual ICollection<ProductSpecification>? ProductSpecifications { get; set; }
        public virtual ICollection<Reviews>? Reviews { get; set; }

    }
}
