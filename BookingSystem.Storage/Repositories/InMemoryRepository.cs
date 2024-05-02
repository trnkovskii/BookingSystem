using BookingSystem.Storage.Interfaces;

namespace BookingSystem.Storage.Repositories
{
    public class InMemoryRepository<TEntity> : IInMemoryRepository<TEntity> where TEntity : class
    {
        private readonly List<TEntity> _storedData;

        public InMemoryRepository()
        {
            _storedData = new List<TEntity>();
        }

        public void StoreData(TEntity value)
        {
            _storedData.Add(value);
        }

        public IEnumerable<TEntity> GetAllData()
        {
            return _storedData;
        }
    }
}
