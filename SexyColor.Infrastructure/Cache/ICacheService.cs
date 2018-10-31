using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.Infrastructure
{
    public interface ICacheService
    {
        void Add(string cacheKey, object value, CachingExpirationType cachingExpirationType);

        void Add(string cacheKey, object value, TimeSpan timeSpan);

        object Get(string cacheKey);

        T Get<T>(string cacheKey) where T : class;

        object GetFromFirstLevel(string cacheKey);

        T GetFromFirstLevel<T>(string cacheKey) where T : class;



        void Remove(string cacheKey);

        void Set(string cacheKey, object value, CachingExpirationType cachingExpirationType);

        void Set(string cacheKey, object value, TimeSpan timeSpan);

        bool EnableDistributedCache { get; }
    }
}
