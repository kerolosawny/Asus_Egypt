

namespace DTO.Reviews
{
    public record ReviewsDTO
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public int ProductId { get; set; }
    }
}
