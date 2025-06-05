
namespace Model
{
    public class ProductSpecification
    {
        public int Id { get; set; }

        [ForeignKey(nameof(SpecificationKey))]
        public int SpecificationKeyId { get; set; }
        public virtual  SpecificationKey SpecificationKey { get; set; }  

        [MaxLength(500)]
        public required string Value { get; set; } 

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual  Product Product { get; set; }
    }

}
