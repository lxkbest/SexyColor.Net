using SexyColor.BusinessComponents;

namespace SexyColor.Web
{
    public class SetGoodsSkuInfoModel
    {
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
        public string SkuName { get; set; }

        /// <summary>
        /// 商品规格进货价
        /// </summary>
        public decimal SkuOriginalPrice { get; set; }

        /// <summary>
        /// 商品规格市场价
        /// </summary>
        public decimal SkuMaketPrice { get; set; }

        /// <summary>
        /// 商品规格销售价
        /// </summary>
        public decimal SkuFactoryPrice { get; set; }

        /// <summary>
        /// 商品规格会员价
        /// </summary>
        public decimal SkuVipPrice { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 规格图
        /// </summary>
        public string SkuImage { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 状态 1=正常计算库存量  2=隐藏不计算库存
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 将GoodsSkuInfo转换成SetGoodsSkuInfoModel
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public SetGoodsSkuInfoModel ToSetGoodsInfoModel(GoodsSkuInfo info)
        {
            Skuid = info.Skuid;
            Goodsid = info.Goodsid;
            SkuName = info.SkuName;
            SkuOriginalPrice = info.SkuOriginalPrice;
            SkuMaketPrice = info.SkuMaketPrice;
            SkuFactoryPrice = info.SkuFactoryPrice;
            SkuVipPrice = info.SkuVipPrice;
            Stock = info.Stock;
            SkuImage = info.SkuImage;
            IsDefault = info.IsDefault;
            Number = info.Number;
            Status = info.Status;
            return this;
        }
    }
}
