using System;
using Akavache;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Assignment.Interfaces;
using Xamarin.Forms;
using Assignment.Services;

[assembly: Dependency(typeof(LocalCache))]

namespace Assignment.Services
{
	public class LocalCache : ILocalCache
	{
        public LocalCache()
        {
            BlobCache.ApplicationName = "Assignment";
        }
 
        public async Task<T> GetCacheItem<T>(string id)
        {
            try
            {
                return await BlobCache.LocalMachine.GetObject<T>(id);
            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }
        }

        public async Task SaveItemInCache<T>(string id, T item)
        {
            await BlobCache.LocalMachine.InsertObject(id, item);
        }
    }
}

