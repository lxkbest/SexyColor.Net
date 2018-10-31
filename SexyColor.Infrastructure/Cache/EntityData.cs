using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace SexyColor.Infrastructure
{
    public class EntityData
    {
        public Type Type { get; }
        public string TypeHashId { get; }
        private RealTimeCacheHelper realTimeCacheHelper;
        private static ConcurrentDictionary<Type, EntityData> entityDatas = new ConcurrentDictionary<Type, EntityData>();

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="t"></param>
        public EntityData(Type t)
        {
            this.Type = t;
            this.TypeHashId = EncryptionUtility.MD5_16(t.FullName);
            RealTimeCacheHelper helper = this.ParseCacheTimelinessHelper(t);
            ICacheService cacheService = DIContainer.Resolve<ICacheService>();
            if (cacheService.EnableDistributedCache)
            {
                cacheService.Set(RealTimeCacheHelper.GetCacheKeyOfTimelinessHelper(this.TypeHashId), helper, CachingExpirationType.Invariable);
            }
            else
            {
                this.realTimeCacheHelper = helper;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static EntityData ForType(System.Type t)
        {
            EntityData data;
            if (!entityDatas.TryGetValue(t, out data) && (data == null))
            {
                data = new EntityData(t);
                entityDatas[t] = data;
            }
            return data;
        }

        /// <summary>
        /// 获取实时缓存组手
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private RealTimeCacheHelper ParseCacheTimelinessHelper(Type t)
        {
            RealTimeCacheHelper helper = null;
            TypeInfo info = t.GetTypeInfo();
            
            IEnumerable<Attribute> customAttributes = info.GetCustomAttributes(typeof(CacheSettingAttribute), true);
            IEnumerator<Attribute> attributes = customAttributes.GetEnumerator();
            if (attributes.MoveNext())
            {
                CacheSettingAttribute attribute = attributes.Current as CacheSettingAttribute;
                if (attribute != null)
                {
                    helper = new RealTimeCacheHelper(attribute.EnableCache, this.TypeHashId);
                    if (attribute.EnableCache)
                    {
                        switch (attribute.ExpirationPolicy)
                        {
                            case CachingExpirationType.Stable:
                                helper.CachingExpirationType = CachingExpirationType.Stable;
                                break;

                            case CachingExpirationType.UsualSingleObject:
                                helper.CachingExpirationType = CachingExpirationType.UsualSingleObject;
                                break;

                            default:
                                helper.CachingExpirationType = CachingExpirationType.SingleObject;
                                break;
                        }
                        List<PropertyInfo> list = new List<PropertyInfo>();
                        if (!string.IsNullOrEmpty(attribute.PropertyNamesOfArea))
                        {
                            foreach (string str in attribute.PropertyNamesOfArea.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                PropertyInfo property = t.GetProperty(str);
                                if (property != null)
                                {
                                    list.Add(property);
                                }
                            }
                        }
                        helper.PropertiesOfArea = list;
                    }
                }
            }
            if (helper == null)
            {
                helper = new RealTimeCacheHelper(true, this.TypeHashId);
            }
            return helper;
        }

        public RealTimeCacheHelper RealTimeCacheHelper
        {
            get
            {
                ICacheService service = DIContainer.Resolve<ICacheService>();
                if (!service.EnableDistributedCache)
                {
                    return this.realTimeCacheHelper;
                }
                string cacheKeyOfTimelinessHelper = RealTimeCacheHelper.GetCacheKeyOfTimelinessHelper(this.TypeHashId);
                RealTimeCacheHelper fromFirstLevel = service.GetFromFirstLevel<RealTimeCacheHelper>(cacheKeyOfTimelinessHelper);
                if (fromFirstLevel == null)
                {
                    fromFirstLevel = this.ParseCacheTimelinessHelper(this.Type);
                    service.Set(cacheKeyOfTimelinessHelper, fromFirstLevel, CachingExpirationType.Invariable);
                }
                return fromFirstLevel;
            }
        }
    }
}
