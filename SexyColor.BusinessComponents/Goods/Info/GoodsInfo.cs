using System;
using MySqlSugar;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:商品信息
    /// </summary>
    [SugarMapping(TableName = "sc_goods_info")]
    [Serializable]
    public class GoodsInfo : IEntity
    {
        #region	构造
        public GoodsInfo()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static GoodsInfo New()
        {

            GoodsInfo goodsInfo = new GoodsInfo();
            goodsInfo.Goodsid = 0;
            goodsInfo.GoodsTypeId = 0;
            goodsInfo.CategoryLevel2Id = 0;
            goodsInfo.Userid = 0;
            goodsInfo.GoodsName = string.Empty;
            goodsInfo.GoodsType = 0;
            goodsInfo.SubjectTitle = string.Empty;
            goodsInfo.BuyCount = 0;
            goodsInfo.RealBuyCount = 0;
            goodsInfo.ImageUrl = string.Empty;
            goodsInfo.PlaceOrigin = string.Empty;
            goodsInfo.TagLabel = string.Empty;
            goodsInfo.ShortName = string.Empty;
            goodsInfo.Stock = 0;
            goodsInfo.Status = 0;
            goodsInfo.IsEnable = false;
            goodsInfo.IsVisible = false;
            goodsInfo.Freight = 0;
            goodsInfo.LimitPurchaseCount = 0;
            goodsInfo.LimitBuyCount = 0;
            goodsInfo.DisplayOrder = 0;
            goodsInfo.DateCreated = DateTime.UtcNow;
            goodsInfo.GoodsPrice = decimal.Zero;
            goodsInfo.GoodsRealPrice = decimal.Zero;
            return goodsInfo;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 商品Id
        /// </summary>
        public long Goodsid { get; set; }

        /// <summary>
        /// 商品类型Id
        /// </summary>
        [SugarMapping(ColumnName = "goods_type_id")]
        public int GoodsTypeId { get; set; }

        /// <summary>
        /// 商品二级分类Id
        /// </summary>
        [SugarMapping(ColumnName = "category_level2_id")]
        public int CategoryLevel2Id { get; set; }

        /// <summary>
        /// 添加人Id
        /// </summary>
        public long Userid { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [SugarMapping(ColumnName = "goods_name")]
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品类型   1.试用 2.众筹 3.销售 4.预定  5.返利
        /// </summary>
        [SugarMapping(ColumnName = "goods_type")]
        public int GoodsType { get; set; }

        /// <summary>
        /// 二级标题
        /// </summary>
        [SugarMapping(ColumnName = "subject_title")]
        public string SubjectTitle { get; set; }

        /// <summary>
        /// 显示购买人数  默认0
        /// </summary>
        [SugarMapping(ColumnName = "buy_count")]
        public int BuyCount { get; set; }

        /// <summary>
        /// 实际购买人数  默认0
        /// </summary>
        [SugarMapping(ColumnName = "real_buy_count")]
        public int RealBuyCount { get; set; }

        /// <summary>
        /// 商品图片   方便程序员使用，商品展示图。 比如 购物车 订单 传递数据时使用的第一张头图展示
        /// </summary>
        [SugarMapping(ColumnName = "image_url")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 原产地   美国，西班牙等 常见 Made in China
        /// </summary>
        [SugarMapping(ColumnName = "place_origin")]
        public string PlaceOrigin { get; set; }

        /// <summary>
        /// 标签   存入标志html或者css，展现淘宝购物列表显示标志
        /// </summary>
        [SugarMapping(ColumnName = "tag_label")]
        public string TagLabel { get; set; }

        /// <summary>
        /// 短名称   G点棒棒，
        /// </summary>
        [SugarMapping(ColumnName = "short_name")]
        public string ShortName { get; set; }

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
        [SugarMapping(ColumnName = "is_enable")]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 前台是否可见 0=否，1=是
        /// </summary>
        [SugarMapping(ColumnName = "is_visible")]
        public bool IsVisible { get; set; }

        /// <summary>
        /// 指定运费  默认为0不指定，不指定将按照全局设置
        /// </summary>
        public int Freight { get; set; }

        /// <summary>
        /// 限购数量  0表示无限制
        /// </summary>
        [SugarMapping(ColumnName = "limit_purchase_count")]
        public int LimitPurchaseCount { get; set; }

        /// <summary>
        /// 限购人数  0表示无限制
        /// </summary>
        [SugarMapping(ColumnName = "limit_buy_count")]
        public int LimitBuyCount { get; set; }
         
        /// <summary>
        /// 排序序号
        /// </summary>
        [SugarMapping(ColumnName = "display_order")]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarMapping(ColumnName = "date_created")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        [SugarMapping(ColumnName = "is_hot")]
        public bool IsHot { get; set; }

        /// <summary>
        /// 商品默认市场价¥39.00
        /// </summary>
        [SugarMapping(ColumnName = "goods_price")]
        public decimal GoodsPrice { get; set; }

        /// <summary>
        /// 商品默认规格价
        /// </summary>
        [SugarMapping(ColumnName = "goods_real_price")]
        public decimal GoodsRealPrice { get; set; }

        /// <summary>
        /// 好评率
        /// </summary>
        [SugarMapping(ColumnName = "commen_rate")]
        public string CommenRate { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        [SugarMapping(ColumnName = "commen_count")]
        public int CommenCount { get; set; }

        /// <summary>
        /// 分类项编号
        /// </summary>
        public int CategoryItemid { get; set; }

        /// <summary>
        /// 特征项编号按,分割
        /// </summary>
        public string Charaid { get; set; }

        /// <summary>
        /// 品牌项编号
        /// </summary>
        public int Brandsid { get; set; }

        #endregion

        #region 扩展属性
        public object EntityId { get => Goodsid; }
        #endregion
    }
}
