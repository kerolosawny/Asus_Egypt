
namespace Model
{
    public class Product_Discount 
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Discount))]
        public int DiscountId { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Discount? discount { get; set; }
        public virtual Product? product { get; set; }
  
    }
}
