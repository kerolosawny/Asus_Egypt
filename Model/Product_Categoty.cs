
namespace Model
{
    public class Product_Categoty
    {
        public int Id { get; set; }
       
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual  Category? Category { get; set; }  
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product? product { get; set; }
    }
}
