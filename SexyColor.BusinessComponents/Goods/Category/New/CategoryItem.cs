using MySqlSugar;
using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.BusinessComponents
{
    [CacheSetting(true)]
    [SugarMapping(TableName = "sc_category_item")]
    [Serializable]
    public class CategoryItem : IEntity
    {
        #region	构造
        public CategoryItem()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static CategoryItem New()
        {
            CategoryItem categoryItem = new CategoryItem();
            categoryItem.CategoryItemId = 0;
            categoryItem.CategoryItemName = string.Empty;
            categoryItem.DisplayOrder = 0;
            categoryItem.Description = string.Empty;
            categoryItem.ImgUrl = string.Empty;
            categoryItem.LinkUrl = 0;
            categoryItem.IsHot = false;
            return categoryItem;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 商品分类项Id
        /// </summary>
        public int CategoryItemId { get; set; }

        /// <summary>
        /// 商品分类项名称
        /// </summary>
        [SugarMapping(ColumnName = "categoryitem_name")]
        public string CategoryItemName { get; set; }

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
        /// 是否热门项
        /// </summary>
        [SugarMapping(ColumnName = "is_hot")]
        public bool IsHot { get; set; }

        /// <summary>
        /// 图片Url
        /// </summary>
        [SugarMapping(ColumnName = "img_url")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 链接Url
        /// </summary>
        [SugarMapping(ColumnName = "link_url")]
        public int LinkUrl { get; set; }
        #endregion

        #region 扩展属性
        public object EntityId { get => CategoryItemId; }
        #endregion
    }
}
