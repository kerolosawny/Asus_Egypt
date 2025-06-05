

namespace DTO.Reviews
{
    public class UpdateReviewsDTO
    {
        [Required]
        public int Id { get; set; }

        [Range(0, 5)]
        public int? Rating { get; set; }

        [MaxLength(500)]
        public string? Comment { get; set; }
    }
}
