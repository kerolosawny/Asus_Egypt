
namespace DTO.Discount
{
    public record DiscountDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;    
        public string Name_EN { get; set; }=string.Empty;
        public int Code { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public bool IsActive { get; set; }
        public decimal Percentage { get; set; }
        public string? ImageUrl { get; set; }

    }
}
