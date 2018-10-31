using System;

namespace SexyColor.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CacheSettingAttribute : Attribute
    {
        private CachingExpirationType expirationPolicy = CachingExpirationType.SingleObject;
        public CacheSettingAttribute(bool enableCache)
        {
            this.EnableCache = enableCache;
        }
        public string PropertyNamesOfArea { get; set; }
        public bool EnableCache { get; private set; }
        public CachingExpirationType ExpirationPolicy
        {
            get
            {
                return this.expirationPolicy;
            }
            set
            {
                this.expirationPolicy = value;
            }
        }
    }
}
