
namespace DTO.Ordere
{
    public class CreateOrderDTO
    {

         [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        [Range(0.01, 1_000_000)] 
        public decimal TotalAmount { get; set; }

        [MaxLength(15)]
        public string? TrackingNumber { get; set; }

        [MaxLength(1500)]
        public string? ShippingAddress { get; set; }

        [Required]
        public ICollection<CreateOrderItemDTO> OrderItems { get; set; }=new List<CreateOrderItemDTO>();

        [Required]
        public int ShippingMethodId { get; set; }

        [Required]
        public int PaymentMethodId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int OrderStateId { get; set; }
    }
}
