namespace SexyColor.BusinessComponents
{
    public class GoodsInfoQuery
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public string Keynum = string.Empty;

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Keyword = string.Empty;

        /// <summary>
        /// 商品价格下限
        /// </summary>
        public decimal? GoodsPriceLowerLimit = null;

        /// <summary>
        /// 商品价格上限
        /// </summary>
        public decimal? GoodsPriceUpperLimit = null;

        /// <summary>
        /// 商品销量下限
        /// </summary>
        public int? BuyCountLowerLimit = null;

        /// <summary>
        /// 商品销量上限
        /// </summary>
        public int? BuyCountUpperLimit = null;
        /// <summary>
        /// 商品状态
        /// </summary>
        public GoodsInfoStatus GoodsInfoStatus = GoodsInfoStatus.Sale;
        /// <summary>
        /// 商品排序
        /// </summary>
        public GoodsInfoSortBy GoodsInfoSortBy =  BusinessComponents.GoodsInfoSortBy.Keynum_DESC;

        /// <summary>
        /// 分类级别
        /// </summary>
        public string CategoryLavel = string.Empty;

    }

    public enum GoodsInfoSortBy
    {
        /// <summary>
        /// 编号升序
        /// </summary>
        Keynum_ASC = 1,
        /// <summary>
        /// 编号降序
        /// </summary>
        Keynum_DESC = 2,
        /// <summary>
        /// 价格升序
        /// </summary>
        Price_ASC = 3,
        /// <summary>
        /// 价格降序
        /// </summary>
        Price_DESC = 4,
        /// <summary>
        /// 总销量升序
        /// </summary>
        BuyCount_ASC = 5,
        /// <summary>
        /// 总销量降序
        /// </summary>
        BuyCount_DESC = 6,
        /// <summary>
        /// 创建时间升序
        /// </summary>
        DateCreated_ASC = 7,
        /// <summary>
        /// 创建时间降序
        /// </summary>
        DateCreated_DESC = 8,
        /// <summary>
        /// 序号升序
        /// </summary>
        DisplayOrder_ASC = 9,
        /// <summary>
        /// 序号降序
        /// </summary>
        DisplayOrder_DESC = 10
    }
}
