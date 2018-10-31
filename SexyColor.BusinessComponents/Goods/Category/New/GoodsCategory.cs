using MySqlSugar;
using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.BusinessComponents
{

    [CacheSetting(true, ExpirationPolicy = CachingExpirationType.Stable)]
    [SugarMapping(TableName = "sc_goods_category")]
    [Serializable]
    public class GoodsCategory : IEntity
    {
        #region	构造
        public GoodsCategory()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static GoodsCategory New()
        {
            GoodsCategory goodsCategory = new GoodsCategory();
            goodsCategory.CategoryId = 0;
            goodsCategory.CategoryName = string.Empty;
            goodsCategory.DisplayOrder = 0;
            goodsCategory.Description = string.Empty;
            return goodsCategory;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 商品一级分类Id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 商品分类名称
        /// </summary>
        [SugarMapping(ColumnName = "category_name")]
        public string CategoryName { get; set; }

        /// <summary>
        /// 排序序号
        /// </summary>
        [SugarMapping(ColumnName = "display_order")]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [SugarMapping(ColumnName = "description")]
        public string Description { get; set; }
        #endregion

        #region 扩展属性
        public object EntityId { get => CategoryId; }
        #endregion
    }
}
