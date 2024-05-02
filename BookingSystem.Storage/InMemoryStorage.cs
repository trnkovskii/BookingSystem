namespace BookingSystem.Storage
{
    public class InMemoryStorage<T> : IInMemoryStorage<T> where T : class
    {
        private readonly List<T> _storedData;

        public InMemoryStorage()
        {
            _storedData = new List<T>();
        }

        public void StoreData(T value)
        {
            _storedData.Add(value);
        }

        public IEnumerable<T> GetAllData()
        {
            return _storedData;
        }
    }
}
