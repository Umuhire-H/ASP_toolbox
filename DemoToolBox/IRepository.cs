using System.Collections.Generic;

namespace DemoToolBox
{
    public interface IRepository<TKey, T>
    {
        IEnumerable<T> Get();
        T Get(TKey key);
        T Insert(T entity);
        bool Update(TKey key, T entity);
        bool Delete(TKey key);
    }
}