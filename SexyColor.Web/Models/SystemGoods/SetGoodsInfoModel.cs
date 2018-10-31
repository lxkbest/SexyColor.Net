using SexyColor.BusinessComponents;
using System;

namespace SexyColor.Web
{
    public class SetGoodsInfoModel
    {

        /// <summary>
        /// 商品Id
        /// </summary>
        public long Goodsid { get; set; }

        /// <summary>
        /// 商品类型Id
        /// </summary>
        public int GoodsTypeId { get; set; }

        /// <summary>
        /// 商品二级分类Id
        /// </summary>
        public int CategoryLevel2Id { get; set; }

        /// <summary>
        /// 商品二级分类名称
        /// </summary>
        public string CategoryLevel2Name { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 二级标题
        /// </summary>
        public string SubjectTitle { get; set; }

        /// <summary>
        /// 显示购买人数  默认0
        /// </summary>
        public int BuyCount { get; set; }

        /// <summary>
        /// 实际购买人数  默认0
        /// </summary>
        public int RealBuyCount { get; set; }

        /// <summary>
        /// 商品图片   方便程序员使用，商品展示图。 比如 购物车 订单 传递数据时使用的第一张头图展示
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 原产地   美国，西班牙等 常见 Made in China
        /// </summary>
        public string PlaceOrigin { get; set; }

        /// <summary>
        /// 总库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 状态  1.待上架，2.售卖中，3.缺少库存下架，4.手动下架
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否上架  0=否 1=是
        /// </summary>
        public int IsEnable { get; set; }

        /// <summary>
        /// 前台是否可见 0=否，1=是
        /// </summary>
        public int IsVisible { get; set; }

        /// <summary>
        /// 指定运费
        /// </summary>
        public int Freight { get; set; }

        /// <summary>
        /// 限购数量  0表示无限制
        /// </summary>
        public int LimitPurchaseCount { get; set; }

        /// <summary>
        /// 排序序号
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        public int IsHot { get; set; }

        /// <summary>
        /// 商品默认市场价¥39.00
        /// </summary>
        public decimal GoodsPrice { get; set; }

        /// <summary>
        /// 商品默认规格价
        /// </summary>
        public decimal GoodsRealPrice { get; set; }


        /// <summary>
        /// 将GoodsInfo转换成SetGoodsInfoModel
        /// </summary>
        /// <returns></returns>
        public SetGoodsInfoModel ToSetGoodsInfoModel(GoodsInfo info)
        {
            Goodsid = info.Goodsid;
            GoodsName = info.GoodsName;
            CategoryLevel2Id = info.CategoryLevel2Id;
            SubjectTitle = info.SubjectTitle;
            BuyCount = info.BuyCount;
            RealBuyCount = info.RealBuyCount;
            ImageUrl = info.ImageUrl;
            PlaceOrigin = info.PlaceOrigin;
 
            Stock = info.Stock;
            Status = info.Status;
            IsEnable = Convert.ToInt32(info.IsEnable);
            IsVisible = Convert.ToInt32(info.IsVisible);
            Freight = info.Freight;
            LimitPurchaseCount = info.LimitPurchaseCount;
            DisplayOrder = info.DisplayOrder;
            IsHot = Convert.ToInt32(info.IsHot);
            GoodsPrice = info.GoodsPrice;
            GoodsRealPrice = info.GoodsRealPrice;
            return this;
        }

        /// <summary>
        /// 将GoodsInfoModel转换成GoodsInfo
        /// </summary>
        /// <returns></returns>
        public GoodsInfo AsGoodsInfo(GoodsInfo info)
        {
            info.Goodsid = Goodsid;
            info.GoodsName = GoodsName;
            info.CategoryLevel2Id = CategoryLevel2Id;
            info.SubjectTitle = SubjectTitle;
            info.BuyCount = BuyCount;
            info.RealBuyCount = RealBuyCount;
            info.PlaceOrigin = PlaceOrigin;
            info.Stock = Stock;
            info.Status = Status;
            info.IsEnable = Convert.ToBoolean(IsEnable);
            info.IsVisible = Convert.ToBoolean(IsVisible);
            info.Freight = Freight;
            info.LimitPurchaseCount = LimitPurchaseCount;
            info.DisplayOrder = DisplayOrder;
            info.IsHot = Convert.ToBoolean(IsHot); ;
            info.GoodsPrice = GoodsPrice;
            info.GoodsRealPrice = GoodsRealPrice;
            return info;
        }
    }
}
