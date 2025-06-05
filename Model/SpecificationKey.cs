
namespace Model
{
    public class SpecificationKey
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public required string Key { get; set; }
        public required string Key_Ar { get; set; }
        public virtual ICollection<ProductSpecification>? ProductSpecifications { get; set; }
        public virtual ICollection<CategorySpecificationKey>? CategorySpecificationKeys { get; set; }
    }
}
