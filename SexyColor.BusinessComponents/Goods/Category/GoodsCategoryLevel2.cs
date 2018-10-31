using MySqlSugar;
using SexyColor.Infrastructure;
using System;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:商品二级分类
    /// </summary>
    [CacheSetting(true, PropertyNamesOfArea = "CategoryLevel1Id")]
    [SugarMapping(TableName = "sc_goods_category_level2")]
    [Serializable]
    public class GoodsCategoryLevel2 : IEntity, ICategoryLevelConstraint
    {
        #region	构造
        public GoodsCategoryLevel2()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static GoodsCategoryLevel2 New()
        {

            GoodsCategoryLevel2 goodsCategoryLevel2 = new GoodsCategoryLevel2();
            goodsCategoryLevel2.CategoryLevel2Id = 0;
            goodsCategoryLevel2.CategoryLevel1Id = 0;
            goodsCategoryLevel2.CategoryName = string.Empty;
            goodsCategoryLevel2.DisplayOrder = 0;
            goodsCategoryLevel2.Description = string.Empty;
            return goodsCategoryLevel2;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 商品二级分类Id
        /// </summary>
        [SugarMapping(ColumnName = "category_level2_id")]
        public int CategoryLevel2Id { get; set; }

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


        [SugarMapping(ColumnName = "category_level2_image_url")]
        public string CategoryLevel2ImageUrl { get; set; }
        #endregion

        #region 扩展属性
        public object EntityId { get => CategoryLevel2Id; }
        #endregion
    }
}
