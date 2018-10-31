using MySqlSugar;
using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.BusinessComponents
{
    [CacheSetting(true, PropertyNamesOfArea = "CategoryId")]
    [SugarMapping(TableName = "sc_category_item_in_chara")]
    [Serializable]
    public class CategoryItemInChara : IEntity
    {
        #region	构造
        public CategoryItemInChara()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static CategoryItemInChara New()
        {
            CategoryItemInChara goodsCategoryInItem = new CategoryItemInChara();
            goodsCategoryInItem.Id = 0;
            goodsCategoryInItem.CharaId = 0;
            goodsCategoryInItem.CategoryItemId = 0;
            return goodsCategoryInItem;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 商品分类项分类特征标识
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 特征Id
        /// </summary>
        public int CharaId { get; set; }
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
