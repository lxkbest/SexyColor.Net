using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Redis;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Linq;

namespace SexyColor.Infrastructure
{
    public class RedisMemoryCache : ICache
    {
        private IDatabase cache;
        private ConnectionMultiplexer connection;
        private readonly string instance;
        private readonly RedisCacheOptions options = new RedisCacheOptions { Configuration = "localhost", InstanceName = "Sexy"  };

        public RedisMemoryCache()
        {
            if (connection == null)
            {
                connection = ConnectionMultiplexer.Connect(options.Configuration);
                cache = connection.GetDatabase();
                instance = options.InstanceName;
            }
        }

        public RedisMemoryCache(RedisCacheOptions options, int database = 0)
        {
            connection = ConnectionMultiplexer.Connect(options.Configuration);
            cache = connection.GetDatabase(database);
            instance = options.InstanceName;
        }

        public string GetKeyForRedis(string key)
        {
            return instance + key;
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存项</param>
        public void Add(string key, object value)
        {
            if (string.IsNullOrEmpty(key) || value == null)
                throw new ArgumentNullException(nameof(key));
            cache.StringSet(GetKeyForRedis(key), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));
        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存项</param>
        /// <param name="expiresSliding">在Redis里面可以无视这个参数</param>
        /// <param name="expiressAbsoulte">过期时长</param>
        public void Add(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (string.IsNullOrEmpty(key) || value == null)
                throw new ArgumentNullException(nameof(key));
            cache.StringSet(GetKeyForRedis(key), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), expiressAbsoulte);
        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存项</param>
        /// <param name="timeSpan">过期时长</param>
        public void Add(string key, object value, TimeSpan timeSpan)
        {
            if (string.IsNullOrEmpty(key) || value == null)
                throw new ArgumentNullException(nameof(key));
            cache.StringSet(GetKeyForRedis(key), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), timeSpan);
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
                return cache.KeyExists(GetKeyForRedis(key));
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
            var value = cache.StringGet(GetKeyForRedis(cacheKey));
            if (!value.HasValue)
                return null;
            return value;
        }
        /// <summary>
        /// 删除缓存项
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        public void Remove(string cacheKey)
        {
            if (string.IsNullOrEmpty(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));
            cache.KeyDelete(GetKeyForRedis(cacheKey));
        }
        /// <summary>
        /// 删除集合缓存项
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        public void RemoveAll(IEnumerable<string> keys)
        {
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));
            keys.ToList().ForEach(cacheKey => Remove(cacheKey));
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
        }
    }
}
