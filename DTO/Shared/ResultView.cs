
namespace DTO.Shared
{
    public class ResultView<T>
    {
        public  T Entity { get; set; }
        public bool IsSuccess { get; set; }
        public string Massage { get; set; } = string.Empty;
    }
}
