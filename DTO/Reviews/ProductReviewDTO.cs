

namespace DTO.Reviews
{
    public class ProductReviewDTO
    {
        [Range(0, 5)]
        public int Rating { get; set; }


        [MaxLength(500)]
        public string? Comment { get; set; }

        [MaxLength(100)]
        public required string Name_EN { get; set; }

        [MaxLength(100)]
        public required string User_Name { get; set; }
        /// <summary>
        /// ///////////////////////////////////
        /// </summary>
        //public string Product { get; set; }
        //public List<ReviewsDTO> Reviews { get; set; }


    }
}
