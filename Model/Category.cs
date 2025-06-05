
namespace Model
{
    public class Category: BaseEntity<int>
    {
        public Category()
        {
            SubCategories=new List<Category>();
            Product_Category = new List<Product_Categoty>();
            CategorySpecificationKeys = new List<CategorySpecificationKey>();
        }
        [MaxLength(100)]
        public required string Name { get; set; }
        [MaxLength(100)]
        public required string Name_EN { get; set; }
        [MaxLength(300)]
        public  string? ImageUrl { get; set; }
        [ForeignKey(nameof(Category))]
        public int? ParentCategoryID { get; set; }
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category>? SubCategories { get; set; }        
        [Range(300, 600)]
        public int? Code { get; set; }
        public virtual ICollection<Product_Categoty>? Product_Category { get; set; }
        public virtual ICollection<CategorySpecificationKey>? CategorySpecificationKeys { get; set; }

    }
}
