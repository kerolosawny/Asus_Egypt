
namespace DTO.Order_State
{
    public class Order_StateDTO
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        [MaxLength(50)]
        public required string Name_EN { get; set; }
    }
}
