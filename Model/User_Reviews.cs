
namespace Model
{
    public class User_Reviews  
    {
        public int Id { get; set; }
         [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User user { get; set; }

        [ForeignKey("Reviews")]
        public int ReviewID { get; set; }
        public virtual Reviews review { get; set; }
    }
}
