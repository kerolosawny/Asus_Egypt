

namespace DTO.Order_State
{
    public class CreateOrder_StateDTO
    {

        public int Id { get; set; }

        [MaxLength(50)]
        [Remote(action: "IsOrderStateNameAvailable", controller: "OrderState", ErrorMessage = "The state name already exists.")]

        public required string Name { get; set; }
        [MaxLength(50)]
        [Remote(action: "IsOrderStateNameENAvailable", controller: "OrderState", ErrorMessage = "The state name already exists.")]

        public required string Name_EN { get; set; }
    }
}
