

namespace DTO.Discount
{
    public class CreateDiscountDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]

        [Remote(action: "IsDiscountNameAvailable", controller: "Discount", ErrorMessage = "The Discount name already exists.")]

        public required string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [Remote(action: "IsDiscountNameENAvailable", controller: "Discount", ErrorMessage = "The Discount name already exists.")]


        public required string Name_EN { get; set; }

         [Range(200, 1000)]  
        public int? Code { get; set; }

        [Required]
        public DateTime Start_date { get; set; }

        [Required]
       // [DateGreaterThan(nameof(StartDate), ErrorMessage = "End date must be after start date.")]
        public DateTime End_date { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Percentage must be between 1 and 100.")]
        public decimal Percentage { get; set; }
        public string? ImageUrl { get; set; }
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg", ".svg" })]
        public IFormFile? ImageData { get; set; }
    }
}
