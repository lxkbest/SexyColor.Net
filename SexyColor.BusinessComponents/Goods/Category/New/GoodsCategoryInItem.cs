using MySqlSugar;
using SexyColor.Infrastructure;
using System;

namespace SexyColor.BusinessComponents
{
    [CacheSetting(true, PropertyNamesOfArea = "CategoryId")]
    [SugarMapping(TableName = "sc_goods_category_in_item")]
    [Serializable]
    public class GoodsCategoryInItem : IEntity
    {
        #region	构造
        public GoodsCategoryInItem()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static GoodsCategoryInItem New()
        {
            GoodsCategoryInItem goodsCategoryInItem = new GoodsCategoryInItem();
            goodsCategoryInItem.Id = 0;
            goodsCategoryInItem.CategoryId = 0;
            goodsCategoryInItem.CategoryItemId = 0;
            return goodsCategoryInItem;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 商品分类分类项屏蔽标识
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 商品分类Id
        /// </summary>
        public int CategoryId { get; set; }
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
