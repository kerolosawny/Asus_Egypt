
namespace DTO.OrderItem
{
    public record OrderItemDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Tax { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; } = string.Empty;

    }
}
