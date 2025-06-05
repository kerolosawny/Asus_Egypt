
namespace Model
{
    public class Order_State 
    {
        [Key]
       public int Id { get; set; }
        [MaxLength(50)]
        public  string? Name { get; set; }
        [MaxLength(50)]
        public  string? Name_EN { get; set; }

    }
}
