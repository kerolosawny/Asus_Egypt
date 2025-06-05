

namespace DTO.OrderItem
{
    public class CreateOrderItemDTO
    {
        [Required]
        [Range(1, int.MaxValue)]  
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, 100000)]  
        public decimal Price { get; set; }

        public int ProductId { get; set; }
        [Required]
        [Range(0, 100)]  
        public int Tax { get; set; }
    }
}
