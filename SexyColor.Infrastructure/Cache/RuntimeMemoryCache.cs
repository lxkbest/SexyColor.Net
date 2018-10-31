
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;


namespace SexyColor.Infrastructure
{
    public class RuntimeMemoryCache : ICache
    {
        private readonly MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存项</param>
        public void Add(string key, object value)
        {
            if (string.IsNullOrEmpty(key) || value == null)
                throw new ArgumentNullException(nameof(key));
            cache.Set(key, value);
        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存项</param>
        /// <param name="expiresSliding">滑动过期</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        public void Add(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (string.IsNullOrEmpty(key) || value == null)
                throw new ArgumentNullException(nameof(key));
            cache.Set(key, value, new MemoryCacheEntryOptions().
               SetSlidingExpiration(expiresSliding).
               SetAbsoluteExpiration(expiressAbsoulte));
        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存项</param>
        /// <param name="timeSpan">绝对过期时长</param>
        public void Add(string key, object value, TimeSpan timeSpan)
        {
            if (string.IsNullOrEmpty(key) || value == null)
                throw new ArgumentNullException(nameof(key));
            cache.Set(key, value, timeSpan);
        }
        /// <summary>
        /// 缓存项是否存在
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                object cached;
                bool isSuccess = cache.TryGetValue(key, out cached);
                return isSuccess;
            }
            throw new ArgumentNullException(nameof(key));
        }
        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        public object Get(string cacheKey)
        {
            if (string.IsNullOrEmpty(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));
            return cache.Get(cacheKey);
        }

 
        /// <summary>
        /// 删除缓存项
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        public void Remove(string cacheKey)
        {
            if (string.IsNullOrEmpty(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));
            cache.Remove(cacheKey);
        }

        /// <summary>
        /// 删除集合缓存项
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        public void RemoveAll(IEnumerable<string> keys)
        {
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));
            keys.ToList().ForEach(item => cache.Remove(item));
        }
        /// <summary>
        /// 修改缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存项</param>
        public void Set(string key, object value)
        {
            if (string.IsNullOrEmpty(key) || value == null)
                throw new ArgumentNullException(nameof(key));
            if (Exists(key))
            {
                Remove(key);
                Add(key, value);
            }
            else
            {
                Add(key, value);
            }
        }
        /// <summary>
        /// 修改缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存项</param>
        /// <param name="timeSpan">绝对时长</param>
        public void Set(string key, object value, TimeSpan timeSpan)
        {
            if (string.IsNullOrEmpty(key) || value == null)
                throw new ArgumentNullException(nameof(key));
            if (Exists(key))
            {
                Remove(key);
                Add(key, value, timeSpan);
            }
            else
            {
                Add(key, value, timeSpan);
            }
        }
        /// <summary>
        /// 修改缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存项</param>
        /// <param name="expiresSliding">滑动时长</param>
        /// <param name="expiressAbsoulte">绝对时长</param>
        public void Set(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (string.IsNullOrEmpty(key) || value == null)
                throw new ArgumentNullException(nameof(key));
            if (Exists(key))
            {
                Remove(key);
                Add(key, value, expiresSliding, expiressAbsoulte);
            }
            else
            {
                Add(key, value, expiresSliding, expiressAbsoulte);
            }
        }
    }
}
