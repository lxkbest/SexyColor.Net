using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySqlSugar;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
	/// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:商品规格信息
    /// </summary>
    [SugarMapping(TableName = "sc_goods_sku_info")]
    [Serializable]
    public class GoodsSkuInfo
    {
		#region	构造
		public GoodsSkuInfo(){
		
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
            return goodsSkuInfo;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 商品规格Id
        /// </summary>
		[SugarMapping(ColumnName = "skuid")]
        public long Skuid { get; set; }
 
        /// <summary>
        /// 商品Id
        /// </summary>
		[SugarMapping(ColumnName = "goodsid")]
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
		[SugarMapping(ColumnName = "stock")]
        public int Stock { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
