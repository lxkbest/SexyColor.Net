using System;
using MySqlSugar;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:商品规格信息
    /// </summary>
    [CacheSetting(true)]
    [SugarMapping(TableName = "sc_goods_sku_info")]
    [Serializable]
    public class GoodsSkuInfo : IEntity
    {
        #region	构造
        public GoodsSkuInfo()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static GoodsSkuInfo New()
        {
            GoodsSkuInfo goodsSkuInfo = new GoodsSkuInfo();
            goodsSkuInfo.Skuid = 0;
            goodsSkuInfo.Goodsid = 0;
            goodsSkuInfo.SkuName = string.Empty;
            goodsSkuInfo.SkuOriginalPrice = decimal.Zero;
            goodsSkuInfo.SkuMaketPrice = decimal.Zero;
            goodsSkuInfo.SkuFactoryPrice = decimal.Zero;
            goodsSkuInfo.SkuVipPrice = decimal.Zero;
            goodsSkuInfo.Stock = 0;
            goodsSkuInfo.SkuImage = string.Empty;
            return goodsSkuInfo;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 商品规格Id
        /// </summary>
        public long Skuid { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public long Goodsid { get; set; }

        /// <summary>
        /// 商品规格名称
        /// </summary>
        [SugarMapping(ColumnName = "sku_name")]
        public string SkuName { get; set; }

        /// <summary>
        /// 商品规格进货价
        /// </summary>
        [SugarMapping(ColumnName = "sku_original_price")]
        public decimal SkuOriginalPrice { get; set; }

        /// <summary>
        /// 商品规格市场价
        /// </summary>
        [SugarMapping(ColumnName = "sku_maket_price")]
        public decimal SkuMaketPrice { get; set; }

        /// <summary>
        /// 商品规格销售价
        /// </summary>
        [SugarMapping(ColumnName = "sku_factory_price")]
        public decimal SkuFactoryPrice { get; set; }

        /// <summary>
        /// 商品规格会员价
        /// </summary>
        [SugarMapping(ColumnName = "sku_vip_price")]
        public decimal SkuVipPrice { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 规格图
        /// </summary>
        [SugarMapping(ColumnName = "sku_image")]
        public string SkuImage { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        [SugarMapping(ColumnName = "is_default")]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 状态 1=正常计算库存量  2=隐藏不计算库存
        /// </summary>
        public int Status { get; set; }
        #endregion

        #region 扩展属性
        public object EntityId { get => Skuid; }
        #endregion
    }
}
