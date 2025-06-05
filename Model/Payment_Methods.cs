
namespace Model
{
    public class Payment_Methods : BaseEntity<int>
    {
        public  string? Card_Number { get; set; }
        public DateOnly? Expiration_Date { get; set; }
        public bool Is_Default { get; set; }=false;
        public required string Name { get; set; }

        [ForeignKey("Card_Type")]
        public int? Card_TypeId { get; set; }
         public virtual Card_Type? Cardtype { get; set; }
     }
}
