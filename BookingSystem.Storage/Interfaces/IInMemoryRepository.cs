namespace BookingSystem.Storage.Interfaces
{
    public interface IInMemoryRepository<TEntity> where TEntity : class
    {
        void StoreData(TEntity value);
        IEnumerable<TEntity> GetAllData();
        void UpdateData(TEntity valueToUpdate, Func<TEntity, bool> predicate);
        TEntity FindById(string propertyName, string id);
    }
}