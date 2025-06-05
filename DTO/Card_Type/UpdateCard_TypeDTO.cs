
namespace DTO.Card_Type
{
    public class UpdateCard_TypeDTO
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }
    }
}
