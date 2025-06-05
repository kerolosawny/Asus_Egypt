
 
namespace Model
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            Orders = new List<Order>();
            UserReviews = new List<User_Reviews>();
        }
        [MaxLength(25)]
        public string? FirstName { get; set; }
        [MaxLength(25)]
        public string? LastName { get; set; }

        [MaxLength(255)]
        public DateTime? BirthDate { get; set; }
        [MaxLength(255)]
        public string? City { get; set; }
        [MaxLength(255)]

        public string? Region { get; set; }
        [MaxLength(25)]

        public string? PostalCode { get; set; }
        [MaxLength(255)]

        public string? Country { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        [ForeignKey(nameof(Payment_Methods))]
        public int? Payment_MethodsID { get; set; } 
        public virtual Payment_Methods? Payment_Methods { get; set; }
        public virtual ICollection<User_Reviews>? UserReviews { get; set; }

    }
}
