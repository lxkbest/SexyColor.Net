using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SexyColor.Web
{
    public class SaveGoodsInfoModel
    {
        /// <summary>
        /// 商品基本信息
        /// </summary>
        public GoodsInfoModel goodsInfoModel { get; set; }

        /// <summary>
        /// 商品SKU属性
        /// </summary>
        public IEnumerable<GoodsSkuInfoModel> goodsSkuInfoModels { get; set; }
    }
}
