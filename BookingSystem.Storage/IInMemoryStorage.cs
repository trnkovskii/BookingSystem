namespace BookingSystem.Storage
{
    public interface IInMemoryStorage<T> where T : class
    {
        void StoreData(T value);
        IEnumerable<T> GetAllData();
    }
}