
namespace Model
{
    public class Product_Image
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public required string image_url { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }  
        public virtual Product? Product { get; set; }
    }
}
