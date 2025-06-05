
namespace DTO.Order_State
{
    public class UpdateOrder_StateDTO
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public required string Name { get; set; }
        [MaxLength(50)]
        [Required]  
        public required string Name_EN { get; set; }
    }
}
