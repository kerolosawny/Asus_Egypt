
namespace Model
{
    public class Discount: BaseEntity<int>
    {
        //public int Id { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; }
        [MaxLength(100)]
        public required string Name_EN { get; set; }
        [MaxLength(300)]
        public string? ImageUrl { get; set; }
        [Range(200, 1_000)]
        public int? Code { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public bool IsActive { get; set; }
        [Range(0, 100)]
        [Column(TypeName = "money")]
        public decimal Percentage { get; set; }
        public virtual ICollection<Product_Discount>? product_Discounts { get; set; }
    }
 
}
