

namespace DTO.Reviews
{
    public class CreateReviewsDTO
    {
        [Required]
        [Range(0, 5)] // Rating should be between 0 and 5
        public required int Rating { get; set; }

        [MaxLength(500)]
        public string? Comment { get; set; }

        [Required]
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
