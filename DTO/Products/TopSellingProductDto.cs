namespace DTO.Products
{
    public class TopSellingProductDto
    {
        public string? ImageUrl { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int QuantitySold { get; set; }
        public decimal Revenue { get; set; }
    }
}
