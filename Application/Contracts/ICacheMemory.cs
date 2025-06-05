
namespace Application.Contracts
{
    public interface ICacheMemory<T>
    {
        //for cash memory
        public void AddToCache(string key, object value, TimeSpan? absoluteExpiration = null);
        public T GetFromCache<T>(string key);
        public void RemoveFromCache(string key);
        public List<T> AddtoCashMemoery(string key, List<T> items);
        public void RemoveFromCashMemoery(string key, T item);

    }
}
