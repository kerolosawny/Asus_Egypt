
namespace Model
{
    public class Reviews : BaseEntity<int>
    {
        public int IsAccpted { get; set; }
        [Range(0, 5)]
        public int Rating { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual ICollection<User_Reviews>? UserReviews { get; set; }
        public virtual required Product product { get; set; }
    }
}
