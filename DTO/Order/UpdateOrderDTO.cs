

namespace DTO.Ordere
{
    public class UpdateOrderDTO
    {
        [Required]
        public int Id { get; set; }

        [Range(1000, int.MaxValue)]
        public int? Code { get; set; }

        public DateTime? OrderDate { get; set; } = DateTime.Now;

        [Range(0.01, 1000000)]
        public decimal? TotalAmount { get; set; }

        [MaxLength(15)]
        public string? TrackingNumber { get; set; }

        [MaxLength(1500)]
        public string? ShippingAddress { get; set; }

        public ICollection<UpdateOrderItemDTO>? OrderItems { get; set; } = new List<UpdateOrderItemDTO>();

        public int ShippingMethodId { get; set; }

        public int PaymentMethodId { get; set; }

        public int OrderStateId { get; set; }
   
    }
}
