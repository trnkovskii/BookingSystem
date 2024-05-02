namespace BookingSystem.Storage.Interfaces
{
    public interface IInMemoryRepository<TEntity> where TEntity : class
    {
        void StoreData(TEntity value);
        IEnumerable<TEntity> GetAllData();
    }
}