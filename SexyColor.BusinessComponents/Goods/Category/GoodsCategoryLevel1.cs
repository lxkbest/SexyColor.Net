using MySqlSugar;
using SexyColor.Infrastructure;
using System;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:商品一级分类
    /// </summary>
    [CacheSetting(true, ExpirationPolicy = CachingExpirationType.Stable)]
    [SugarMapping(TableName = "sc_goods_category_level1")]
    [Serializable]
    public class GoodsCategoryLevel1 : IEntity, ICategoryLevelConstraint
    {
        #region	构造
        public GoodsCategoryLevel1()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static GoodsCategoryLevel1 New()
        {

            GoodsCategoryLevel1 goodsCategoryLevel1 = new GoodsCategoryLevel1();
            goodsCategoryLevel1.CategoryLevel1Id = 0;
            goodsCategoryLevel1.CategoryName = string.Empty;
            goodsCategoryLevel1.DisplayOrder = 0;
            goodsCategoryLevel1.Description = string.Empty;
            return goodsCategoryLevel1;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 商品一级分类Id
        /// </summary>
        [SugarMapping(ColumnName = "category_level1_id")]
        public int CategoryLevel1Id { get; set; }

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

        /// <summary>
        /// 图片
        /// </summary>
        [SugarMapping(ColumnName = "category_level1_image_url")]
        public string CategoryLevel1ImageUrl { get; set; }
        #endregion

        #region 扩展属性
        public object EntityId { get => CategoryLevel1Id; }
        #endregion
    }
}
