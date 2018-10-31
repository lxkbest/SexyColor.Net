using System;
using MySqlSugar;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:sc_goods_collection_logs
    /// </summary>
    [SugarMapping(TableName = "sc_goods_collection_logs")]
    [Serializable]
    public class GoodsCollectionLogs : IEntity
    {
        #region	构造
        public GoodsCollectionLogs()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static GoodsCollectionLogs New()
        {

            GoodsCollectionLogs goodsCollectionLogs = new GoodsCollectionLogs();
            goodsCollectionLogs.Id = 0;
            goodsCollectionLogs.Goodsid = 0;
            goodsCollectionLogs.Userid = 0;
            goodsCollectionLogs.GoodsName = string.Empty;
            goodsCollectionLogs.GoodsPrice = decimal.Zero;
            goodsCollectionLogs.GoodsImageUrl = string.Empty;
            goodsCollectionLogs.DateCreated = DateTime.UtcNow;
            return goodsCollectionLogs;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 商品收藏记录标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public long Goodsid { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long Userid { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [SugarMapping(ColumnName = "goods_name")]
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        [SugarMapping(ColumnName = "goods_price")]
        public decimal GoodsPrice { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        [SugarMapping(ColumnName = "goods_image_url")]
        public string GoodsImageUrl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime DateCreated { get; set; }


        #endregion

        #region 扩展属性
        public object EntityId { get => Id; }
        #endregion
    }
}
