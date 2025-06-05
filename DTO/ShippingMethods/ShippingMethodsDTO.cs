
namespace DTO.ShippingMethods
{
    public class ShippingMethodsDTO
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public required string Method_Name { get; set; }
        public decimal Cost { get; set; }
        public DateTime Estimated_Delivery_Time { get; set; }

    }
}
