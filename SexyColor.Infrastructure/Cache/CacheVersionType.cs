namespace SexyColor.Infrastructure
{
    /// <summary>
    /// 缓存版本类型
    /// </summary>
    public enum CacheVersionType
    {
        /// <summary>
        /// 无版本
        /// </summary>
        None,
        /// <summary>
        /// 全局版本
        /// </summary>
        GlobalVersion,
        /// <summary>
        /// 分区版本
        /// </summary>
        AreaVersion
    }
}
