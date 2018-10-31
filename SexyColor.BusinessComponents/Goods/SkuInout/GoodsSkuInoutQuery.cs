namespace SexyColor.BusinessComponents
{
    public class GoodsSkuInoutQuery
    {

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName = string.Empty;

        /// <summary>
        /// 商品规格名称
        /// </summary>
        public string SkuName = string.Empty;

        /// <summary>
        /// 出入库数量min  默认0表示 没有自己插入该字段数据
        /// </summary>
        public int? InoutNumberMin = null;

        /// <summary>
        /// 出入库数量max  默认0表示 没有自己插入该字段数据
        /// </summary>
        public int? InoutNumberMax = null;

        /// <summary>
        /// 是否出库   0.否=入库，1.是=出库
        /// </summary>
        public bool? IsOut = null;

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? DateCreated = null;

        public GoodsSkuInoutSortBy? GoodsSkuInoutSortBy = null;
    }

    /// <summary>
    /// 排序方式
    /// </summary>
    public enum GoodsSkuInoutSortBy
    {
        /// <summary>
        /// 根据创建时间排序
        /// </summary>
        DateCreated = 1,

        /// <summary>
        /// 根据创建时间倒序排列
        /// </summary>
        DateCreated_Desc = 2,

        /// <summary>
        /// 根据是否启用排序
        /// </summary>
        IsEnabled = 3,
    }
}
