using System;
using MySqlSugar;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:商品规格出入库记录
    /// </summary>
    [CacheSetting(true)]
    [SugarMapping(TableName = "sc_goods_sku_inout")]
    [Serializable]
    public class GoodsSkuInout : IEntity
    {
        #region	构造
        public GoodsSkuInout()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static GoodsSkuInout New()
        {

            GoodsSkuInout goodsSkuInout = new GoodsSkuInout();
            goodsSkuInout.Id = 0;
            goodsSkuInout.Goodsid = 0;
            goodsSkuInout.Skuid = 0;
            goodsSkuInout.InoutNumber = 0;
            goodsSkuInout.IsOut = false;
            goodsSkuInout.DateCreated = DateTime.UtcNow;
            return goodsSkuInout;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 出入库记录标识
        /// </summary>
        [SugarMapping(ColumnName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        [SugarMapping(ColumnName = "goodsid")]
        public long Goodsid { get; set; }

        /// <summary>
        /// 商品规格Id
        /// </summary>
        [SugarMapping(ColumnName = "skuid")]
        public long Skuid { get; set; }

        /// <summary>
        /// 出入库数量  默认0表示 没有自己插入该字段数据
        /// </summary>
        [SugarMapping(ColumnName = "inout_number")]
        public int InoutNumber { get; set; }

        /// <summary>
        /// 是否出库   0.否=入库，1.是=出库
        /// </summary>
        [SugarMapping(ColumnName = "is_out")]
        public bool IsOut { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarMapping(ColumnName = "date_created")]
        public System.DateTime DateCreated { get; set; }

        /// <summary>
        /// 操作记录
        /// </summary>
        public string Operation { get; set; }

        #endregion

        #region 扩展属性
        public object EntityId { get => Id; }
        #endregion
    }
}
