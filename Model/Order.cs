namespace Model
{
    public class Order : BaseEntity<int>
    {
       
        [Range(1000, int.MaxValue)]
        public int? Code { get; set; }
        public DateTime order_date { get; set; }
        [Column(TypeName = "money")]
        public decimal Total_amout { get; set; }
        [MaxLength(15)]
        public string? tracking_number { get; set; }
        [MaxLength(1500)]
        public string? shipping_address { get; set; }
        [ForeignKey(nameof(Shipping_Methods))]
        public int ShippingMethodsID { get; set; }
        public virtual Shipping_Methods? shipping_Methods {  get; set; }
        [ForeignKey(nameof(Payment_Methods))]
        public int PaymentMethodsID { get; set; }
        public virtual Payment_Methods? payment_Methods { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        [ForeignKey(nameof(Order_State))]
        public int OrderStateID { get; set; }
        public virtual  Order_State? order_State { get; set; }
        public virtual ICollection<Order_Items>? Order_Items { get; set; }= new List<Order_Items>();


    }
}
