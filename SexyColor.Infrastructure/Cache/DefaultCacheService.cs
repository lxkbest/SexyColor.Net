using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.Infrastructure
{
    public class DefaultCacheService : ICacheService
    {
        private ICache cache;
        private readonly Dictionary<CachingExpirationType, TimeSpan> cachingExpirationDictionary;
        private bool enableDistributedCache;
        private ICache localCache;

        public DefaultCacheService(ICache cache, float cacheExpirationFactor) : this(cache, cache, cacheExpirationFactor, false)
        {
        }

        public DefaultCacheService(ICache cache, ICache localCache, float cacheExpirationFactor, bool enableDistributedCache)
        {
            this.cache = cache;
            this.localCache = localCache;
            this.enableDistributedCache = enableDistributedCache;
            cachingExpirationDictionary = new Dictionary<CachingExpirationType, TimeSpan>();
            cachingExpirationDictionary.Add(CachingExpirationType.Invariable, new TimeSpan(0, 0, (int)(86400f * cacheExpirationFactor)));
            cachingExpirationDictionary.Add(CachingExpirationType.Stable, new TimeSpan(0, 0, (int)(28800f * cacheExpirationFactor)));
            cachingExpirationDictionary.Add(CachingExpirationType.RelativelyStable, new TimeSpan(0, 0, (int)(7200f * cacheExpirationFactor)));
            cachingExpirationDictionary.Add(CachingExpirationType.UsualSingleObject, new TimeSpan(0, 0, (int)(600f * cacheExpirationFactor)));
            cachingExpirationDictionary.Add(CachingExpirationType.UsualObjectCollection, new TimeSpan(0, 0, (int)(300f * cacheExpirationFactor)));
            cachingExpirationDictionary.Add(CachingExpirationType.SingleObject, new TimeSpan(0, 0, (int)(180f * cacheExpirationFactor)));
            cachingExpirationDictionary.Add(CachingExpirationType.ObjectCollection, new TimeSpan(0, 0, (int)(180f * cacheExpirationFactor)));
        }

        public bool EnableDistributedCache { get => enableDistributedCache; }

        public void Add(string cacheKey, object value, CachingExpirationType cachingExpirationType)
        {
            Add(cacheKey, value, cachingExpirationDictionary[cachingExpirationType]);
        }

        public void Add(string cacheKey, object value, TimeSpan timeSpan)
        {
            cache.Add(cacheKey, value, timeSpan);
        }

        public void Clear()
        {
            
        }
        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        public object Get(string cacheKey)
        {
            object obj = null;
            //判断是否使用分布式缓存
            if (EnableDistributedCache)
            {
                //在本地缓存中获取
                obj = localCache.Get(cacheKey);
            }
            if (obj == null)
            {
                //在分布式环境中获取
                obj = cache.Get(cacheKey);
                //并且在本地缓存也维护一份
                if (EnableDistributedCache)
                {
                    if(obj != null)
                        localCache.Add(cacheKey, obj, this.cachingExpirationDictionary[CachingExpirationType.SingleObject]);
                }
            }
            return obj;
        }
        /// <summary>
        /// 获取缓存项（泛型）
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        public T Get<T>(string cacheKey) where T : class
        {
            object obj = Get(cacheKey);
            if (obj != null)
            {
                return (obj as T);
            }
            return default(T);
        }

        /// <summary>
        /// 获取缓存项穿透1级直接使用缓存
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        public object GetFromFirstLevel(string cacheKey)
        {
            return cache.Get(cacheKey);
        }
        /// <summary>
        /// 穿透1级直接使用缓存（泛型）
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        public T GetFromFirstLevel<T>(string cacheKey) where T : class
        {
            object fromFirstLevel = GetFromFirstLevel(cacheKey);
            if (fromFirstLevel != null)
            {
                return (fromFirstLevel as T);
            }
            return default(T);
        }

        /// <summary>
        /// 移除缓存项
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        public void Remove(string cacheKey)
        {
            cache.Remove(cacheKey);
        }
        /// <summary>
        /// 设置缓存项
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="value">缓存项</param>
        /// <param name="cachingExpirationType">缓存类型</param>
        public void Set(string cacheKey, object value, CachingExpirationType cachingExpirationType)
        {
            Set(cacheKey, value, cachingExpirationDictionary[cachingExpirationType]);
        }
        /// <summary>
        /// 设置缓存项
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="value">缓存项</param>
        /// <param name="timeSpan">时长</param>
        public void Set(string cacheKey, object value, TimeSpan timeSpan)
        {
            cache.Set(cacheKey, value, timeSpan);
        }
    }
}
