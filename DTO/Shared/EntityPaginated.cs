namespace DTO.Shared
{
    public class EntityPaginated<T>
    {
        public required List<T> Data { get; set; }
       
        public int? CurrentPage { get; set; }
        public int? TotalPages { get; set; }
        public int? ItemsPerPage { get; set; } = 15;
    }
}
