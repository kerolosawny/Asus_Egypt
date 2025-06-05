 
namespace Model
{
    public class Card_Type  
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Name { get; set; }
    }
        //[ForeignKey("Card_Type")]
        //public int Payment_MethodId { get; set; }
        //public virtual Payment_Methods? payment_Method { get; set; }
    
}
