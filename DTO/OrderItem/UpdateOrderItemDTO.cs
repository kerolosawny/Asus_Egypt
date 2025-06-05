
namespace DTO.OrderItem
{
    public class UpdateOrderItemDTO
    {
        [Required]
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int? Quantity { get; set; }
        public int ProductId { get; set; }
        [Range(0.01, 100000)]
        public decimal? Price { get; set; }

        [Range(0, 100)]
        public int? Tax { get; set; }
    }
}
