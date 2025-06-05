

namespace DTO.ShippingMethods
{
    public class CreateShippingMethodsDTO
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        [Remote(action: "IsShippingNameAvailable", controller: "Shipping_method", ErrorMessage = "The Shipping Method name already exists.")]

        public required string Method_Name { get; set; }
        public decimal Cost { get; set; }
        public DateTime Estimated_Delivery_Time { get; set; }

    }
}
