using MySqlSugar;
using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.BusinessComponents
{
    [CacheSetting(true, PropertyNamesOfArea = "CategoryId")]
    [SugarMapping(TableName = "sc_category_item_in_brands")]
    [Serializable]
    public class CategoryItemInBrands : IEntity
    {
        #region	构造
        public CategoryItemInBrands()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static CategoryItemInBrands New()
        {
            CategoryItemInBrands goodsCategoryInItem = new CategoryItemInBrands();
            goodsCategoryInItem.Id = 0;
            goodsCategoryInItem.BrandsId = 0;
            goodsCategoryInItem.CategoryItemId = 0;
            return goodsCategoryInItem;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 商品分类项分类品牌标识
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 品牌Id
        /// </summary>
        public int BrandsId { get; set; }
        /// <summary>
        /// 商品分类项Id
        /// </summary>
        public int CategoryItemId { get; set; }

        #endregion

        #region 扩展属性
        public object EntityId { get => Id; }
        #endregion
    }
}
