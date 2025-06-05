
 
namespace Model
{
    public class Order_Items
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "money")]

        public decimal Price { get; set; }
        public int Tax { get; set; }
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public virtual required Order Orders { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }  
        public virtual Product Products { get; set; }

    }
}
