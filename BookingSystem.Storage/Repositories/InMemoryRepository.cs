using BookingSystem.Storage.Interfaces;

namespace BookingSystem.Storage.Repositories
{
    public class InMemoryRepository<TEntity> : IInMemoryRepository<TEntity> where TEntity : class
    {
        private readonly List<TEntity> _storedData = new();

        public void StoreData(TEntity value)
        {
            _storedData.Add(value);
        }

        public IEnumerable<TEntity> GetAllData()
        {
            return _storedData;
        }

        public void UpdateData(TEntity valueToUpdate, Func<TEntity, bool> predicate)
        {
            var existingItem = _storedData.FirstOrDefault(predicate);
            if (existingItem != null)
            {
                var index = _storedData.IndexOf(existingItem);
                _storedData[index] = valueToUpdate;
            }
            else
            {
                throw new InvalidOperationException("No matching item found to update.");
            }
        }

        public TEntity FindById(string propertyName, string id)
        {
            return _storedData.FirstOrDefault(entity => entity.GetType().GetProperty(propertyName)?.GetValue(entity, null)?.Equals(id) ?? false);
        }
    }
}
