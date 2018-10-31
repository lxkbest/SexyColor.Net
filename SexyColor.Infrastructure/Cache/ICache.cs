using System;
using System.Collections.Generic;

namespace SexyColor.Infrastructure
{
    public interface ICache
    {
        void Add(string key, object value);
        void Add(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);
        void Add(string key, object value, TimeSpan timeSpan);
        bool Exists(string key);
        object Get(string cacheKey);

        void Remove(string cacheKey);
        void RemoveAll(IEnumerable<string> keys);
        void Set(string key, object value);
        void Set(string key, object value, TimeSpan timeSpan);
        void Set(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);
    }
}
