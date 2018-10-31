
namespace SexyColor.Web
{
    public class GoodsSkuInfoModel
    {
        /// <summary>
        /// 商品规格名称
        /// </summary>
        public string SkuName { get; set; }

        /// <summary>
        /// 商品规格进货价
        /// </summary>
        public string SkuOriginalPrice { get; set; }

        /// <summary>
        /// 商品规格市场价
        /// </summary>
        public string SkuMaketPrice { get; set; }

        /// <summary>
        /// 商品规格销售价
        /// </summary>
        public int SkuFactoryPrice { get; set; }

        /// <summary>
        /// 商品规格会员价
        /// </summary>
        public int SkuVipPrice { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public bool Stock { get; set; }


    }
}
