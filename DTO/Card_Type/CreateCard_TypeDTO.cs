
namespace DTO.Card_Type
{
    public class CreateCard_TypeDTO
    {
        [Required]
        [Remote(action: "IsCardNameAvailable", controller: "CardType", ErrorMessage = "The Card name already exists.")]
        public required string Name { get; set; }
    }
}
