
namespace DTO.Discount
{
    public class UpdateDiscountDTO
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]

        public string? Name { get; set; }

        [Required]
        [MaxLength(100)]

        public string? Name_EN { get; set; }

        [Range(200, 1000)]
        public int? Code { get; set; }

        public DateTime? Start_date { get; set; }
        public string? ImageUrl { get; set; }
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg", ".svg" })]
        public IFormFile? ImageData { get; set; }
        // [DateGreaterThan(nameof(StartDate), ErrorMessage = "End date must be after start date.")]
        public DateTime? End_date { get; set; }

        public bool IsActive { get; set; }

        [Range(1, 100, ErrorMessage = "Percentage must be between 1 and 100.")]
        public decimal? Percentage { get; set; }
    }
}
