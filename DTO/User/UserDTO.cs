
namespace DTO.Usere
{
    public record UserDTO
    {
       
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public string? Roles { get; set; }
        public ICollection<OrderDTO>? Orders { get; set; }
        //public List<string> Roles { get; set; } 
        [MaxLength(255)]
        public DateTime? BirthDate { get; set; }
        [MaxLength(255)]
        public string? City { get; set; }
        [MaxLength(255)]

        public string? Region { get; set; }
        [MaxLength(25)]

        public string? PostalCode { get; set; }
        [MaxLength(255)]

        public string? Country { get; set; }
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
