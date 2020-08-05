using Microsoft.Extensions.Caching.Memory;
using System;

namespace ApiCommoditiesBr.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        private IMemoryCache _memoryCache;

        public BaseRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        protected T GetInMemoryCache<T>(string indexName) where T : class
        {
            return _memoryCache.Get<T>(indexName);
        }

        protected T SetInMemoryCache<T>(T obj, string indexName, int expTime = 15) where T : class
        {
            return _memoryCache.Set(indexName, obj, TimeSpan.FromMinutes(expTime));
        }
    }
}
