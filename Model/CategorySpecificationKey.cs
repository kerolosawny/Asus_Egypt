
namespace Model
{
    public class CategorySpecificationKey 
    {
        [Key]
        public int Id { get; set; } 
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [ForeignKey(nameof(SpecificationKey))]
        public int SpecificationKeyId { get; set; }
        public virtual SpecificationKey SpecificationKey { get; set; }
    }
}
