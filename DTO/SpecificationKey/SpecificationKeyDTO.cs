

namespace DTO.SpecificationKey
{
    public class SpecificationKeyDTO
    {
        public int Id { get; set; }  

        [MaxLength(100)]
        public required string Key { get; set; }
        [MaxLength(100)]
        public required string Key_Ar { get; set; }

        public List<int>? SubCategoryId { get; set; } 
        public string SubCategoryName { get; set; }=string.Empty;
        public List<string>? values { get; set; }
    }

}
