
namespace Model
{
    [NotMapped]
    public class BaseEntity<T>
    {
        public  T Id { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? Created_at { get; set; }=DateTime.Now;
        public int? Updated_By { get; set; }
        public DateTime? Updated_at { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
