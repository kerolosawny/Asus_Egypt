
namespace Model
{
    public class Shipping_Methods: BaseEntity<int>
    {
         [MaxLength(50)]
        public required string Method_Name { get; set; }
        [Column(TypeName = "money")]
        public decimal Cost { get; set; }
        public DateTime Estimated_Delivery_Time { get; set; }

    }
}
