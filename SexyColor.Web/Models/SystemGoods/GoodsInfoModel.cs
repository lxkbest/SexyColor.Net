using SexyColor.BusinessComponents;

namespace SexyColor.Web
{
    public class GoodsInfoModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 二级分类id
        /// </summary>
        public int CategoryLevel2Id { get; set; }

        /// <summary>
        /// 二级标题
        /// </summary>
        public string SubjectTitle { get; set; }

        /// <summary>
        /// 原产地
        /// </summary>
        public string PlaceOrigin { get; set; }

        /// <summary>
        /// 限购数量
        /// </summary>
        public int LimitPurchaseCount { get; set; }

        /// <summary>
        /// 排序序号
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 是否热门商品
        /// </summary>
        public bool IsHot { get; set; }

        /// <summary>
        /// 前台是否可见
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// 指定运费
        /// </summary>
        public int Freight { get; set; }

        /// <summary>
        /// 将GoodsInfoModel转换成GoodsInfo
        /// </summary>
        /// <returns></returns>
        public GoodsInfo AsGoodsInfo()
        {
            GoodsInfo info = GoodsInfo.New();
            info.GoodsName = GoodsName;
            info.SubjectTitle = SubjectTitle;
            info.PlaceOrigin = PlaceOrigin;
            info.LimitPurchaseCount = LimitPurchaseCount;
            info.IsHot = IsHot;
            info.IsVisible = IsVisible;
            info.Freight = Freight;
            info.DisplayOrder = DisplayOrder;
            info.CategoryLevel2Id = CategoryLevel2Id;
            return info;
        }
    }
}
