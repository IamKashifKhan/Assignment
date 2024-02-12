using System;
using System.Threading.Tasks;

namespace Assignment.Interfaces
{
    public interface ILocalCache
    {
        Task<T> GetCacheItem<T>(string id);
        Task SaveItemInCache<T>(string id, T item);
    }
}

