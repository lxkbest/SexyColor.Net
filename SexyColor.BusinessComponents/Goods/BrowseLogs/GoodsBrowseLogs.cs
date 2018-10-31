using System;
using MySqlSugar;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    [SugarMapping(TableName = "sc_goods_browse_logs")]
    [Serializable]
    public class GoodsBrowseLogs : IEntity
    {
        #region	构造
        public GoodsBrowseLogs()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static GoodsBrowseLogs New()
        {

            GoodsBrowseLogs goodsBrowseLogs = new GoodsBrowseLogs();
            goodsBrowseLogs.Id = 0;
            goodsBrowseLogs.Goodsid = 0;
            goodsBrowseLogs.Userid = 0;
            goodsBrowseLogs.GoodsName = string.Empty;
            goodsBrowseLogs.GoodsPrice = decimal.Zero;
            goodsBrowseLogs.GoodsImageUrl = string.Empty;
            goodsBrowseLogs.DateCreated = DateTime.UtcNow;
            return goodsBrowseLogs;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 商品浏览记录标识
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
        [SugarMapping(ColumnName = "date_created")]
        public System.DateTime DateCreated { get; set; }



        #endregion

        #region 扩展属性
        public object EntityId { get => Id; }
        #endregion
    }
}
