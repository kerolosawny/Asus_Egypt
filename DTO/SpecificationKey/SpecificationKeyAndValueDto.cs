namespace DTO.SpecificationKey
{
    public record SpecificationKeyAndValueDto
    {
        public required string Key { get; set; }
        public List<string> Values { get; set; } = new List<string>();
    }
}
