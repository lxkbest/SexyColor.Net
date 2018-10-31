using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SexyColor.Infrastructure
{
    public class RealTimeCacheHelper
    {
        public bool EnableCache { get; }
        public string TypeHashId { get; }
        public CachingExpirationType CachingExpirationType { get; set; }
        public IEnumerable<PropertyInfo> PropertiesOfArea { get; set; }

        private ConcurrentDictionary<string, ConcurrentDictionary<int, int>> areaVersionDictionary = new ConcurrentDictionary<string, ConcurrentDictionary<int, int>>();
        private ConcurrentDictionary<object, int> entityVersionDictionary = new ConcurrentDictionary<object, int>();
        private ICacheService cacheService = DIContainer.Resolve<ICacheService>();
        private int globalVersion;

        public RealTimeCacheHelper(bool enableCache, string typeHashId)
        {
            this.EnableCache = enableCache;
            this.TypeHashId = typeHashId;
        }

        /// <summary>
        /// 自己缓存Key
        /// </summary>
        /// <param name="typeHashId"></param>
        /// <returns></returns>
        internal static string GetCacheKeyOfTimelinessHelper(string typeHashId)
        {
            return ("CacheTimelinessHelper:" + typeHashId);
        }

        /// <summary>
        /// 根据主键获取实体缓存Key
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public string GetCacheKeyOfEntity(object primaryKey)
        {
            if (this.cacheService.EnableDistributedCache)
            {
                return string.Concat(new object[] { this.TypeHashId, ":", primaryKey, ":", this.GetEntityVersion(primaryKey) });
            }
            var str = (this.TypeHashId + ":" + primaryKey);
            return (this.TypeHashId + ":" + primaryKey);
        }

        /// <summary>
        /// 根据主键获取实体版本号
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public int GetEntityVersion(object primaryKey)
        {
            int num = 0;
            this.entityVersionDictionary.TryGetValue(primaryKey, out num);
            return num;
        }

        /// <summary>
        /// 获取全局版本
        /// </summary>
        /// <returns></returns>
        public int GetGlobalVersion()
        {
            return this.globalVersion;
        }

        /// <summary>
        /// 根据缓存版本类型获取列表缓存Key前缀
        /// </summary>
        /// <param name="cacheVersionType">缓存版本类型</param>
        /// <returns></returns>
        public string GetListCacheKeyPrefix(CacheVersionType cacheVersionType)
        {
            return this.GetListCacheKeyPrefix(cacheVersionType, null, null);
        }

        /// <summary>
        /// 根据缓存版本类型获取列表缓存Key前缀
        /// </summary>
        /// <param name="cacheVersionType">缓存版本类型</param>
        /// <param name="areaCachePropertyName">分区属性名</param>
        /// <param name="areaCachePropertyValue">分区属性值</param>
        /// <returns></returns>
        public string GetListCacheKeyPrefix(CacheVersionType cacheVersionType, string areaCachePropertyName, object areaCachePropertyValue)
        {
            StringBuilder builder = new StringBuilder(this.TypeHashId);
            builder.Append("-L:");
            switch (cacheVersionType)
            {
                case CacheVersionType.GlobalVersion:
                    builder.AppendFormat("{0}:", this.GetGlobalVersion());
                    break;

                case CacheVersionType.AreaVersion:
                    builder.AppendFormat("{0}-{1}-{2}:", areaCachePropertyName, areaCachePropertyValue, this.GetAreaVersion(areaCachePropertyName, areaCachePropertyValue));
                    break;
            }
            return builder.ToString();
        }

        public int GetAreaVersion(string propertyName, object propertyValue)
        {
            int num = 0;
            if (!string.IsNullOrEmpty(propertyName))
            {
                ConcurrentDictionary<int, int> dictionary;
                propertyName = propertyName.ToLower();
                if (this.areaVersionDictionary.TryGetValue(propertyName, out dictionary))
                {
                    dictionary.TryGetValue(propertyValue.GetHashCode(), out num);
                }
            }
            return num;
        }

        public void IncreaseGlobalVersion()
        {
            globalVersion++;
        }

        public void IncreaseEntityCacheVersion(object entityId)
        {
            if (this.cacheService.EnableDistributedCache)
            {
                int num;
                if (this.entityVersionDictionary.TryGetValue(entityId, out num))
                {
                    num++;
                }
                else
                {
                    num = 1;
                }
                this.entityVersionDictionary[entityId] = num;
                this.OnChanged();
            }
        }

        public void IncreaseListCacheVersion(IEntity entity)
        {
            if (PropertiesOfArea != null)
            {
                foreach (PropertyInfo info in PropertiesOfArea)
                {
                    object obj2 = info.GetValue(entity, null);
                    if (obj2 != null)
                    {
                        this.IncreaseAreaVersion(info.Name.ToLower(), new object[] { obj2 }, false);
                    }
                }
            }
            IncreaseGlobalVersion();
            OnChanged();
        }

        /// <summary>
        /// 递增列表缓存区域version
        /// </summary>
        /// <param name="propertyName">分区属性名称</param>
        /// <param name="propertyValue">分区属性值</param>
        public void IncreaseAreaVersion(string propertyName, object propertyValue)
        {
            if (propertyValue != null)
            {
                this.IncreaseAreaVersion(propertyName, new object[] { propertyValue }, true);
            }
        }

        /// <summary>
        /// 递增列表缓存区域version
        /// </summary>
        /// <param name="propertyName">分区属性名称</param>
        /// <param name="propertyValues">多个分区属性值</param>
        public void IncreaseAreaVersion(string propertyName, IEnumerable<object> propertyValues)
        {
            this.IncreaseAreaVersion(propertyName, propertyValues, true);
        }

        private void IncreaseAreaVersion(string propertyName, IEnumerable<object> propertyValues, bool raiseChangeEvent)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                ConcurrentDictionary<int, int> dictionary;
                propertyName = propertyName.ToLower();
                int num = 0;
                if (!this.areaVersionDictionary.TryGetValue(propertyName, out dictionary))
                {
                    this.areaVersionDictionary[propertyName] = new ConcurrentDictionary<int, int>();
                    dictionary = this.areaVersionDictionary[propertyName];
                }
                foreach (object obj2 in propertyValues)
                {
                    int hashCode = obj2.GetHashCode();
                    if (dictionary.TryGetValue(hashCode, out num))
                    {
                        num++;
                    }
                    else
                    {
                        num = 1;
                    }
                    dictionary[hashCode] = num;
                }
                if (raiseChangeEvent)
                {
                    this.OnChanged();
                }
            }
        }


        private void OnChanged()
        {
            if (this.cacheService.EnableDistributedCache)
            {
                this.cacheService.Set(GetCacheKeyOfTimelinessHelper(this.TypeHashId), this, CachingExpirationType.Invariable);
            }
        }
    }
}
