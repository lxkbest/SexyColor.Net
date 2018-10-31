using MySqlSugar;
using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.BusinessComponents
{
    [CacheSetting(true, ExpirationPolicy = CachingExpirationType.Stable)]
    [SugarMapping(TableName = "sc_category_item_brands")]
    [Serializable]
    public class CategoryItemBrands : IEntity
    {
        #region	构造
        public CategoryItemBrands()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static CategoryItemBrands New()
        {
            CategoryItemBrands categoryItemBrands = new CategoryItemBrands();
            categoryItemBrands.BrandsId = 0;
            categoryItemBrands.BrandsName = string.Empty;
            categoryItemBrands.DeteCreated = DateTime.UtcNow;
            return categoryItemBrands;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 品牌Id
        /// </summary>
        public int BrandsId { get; set; }
        /// <summary>
        /// 品牌名称
        /// </summary>
        [SugarMapping(ColumnName = "brands_name")]
        public string BrandsName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarMapping(ColumnName = "dete_created")]
        public DateTime DeteCreated { get; set; }
        #endregion

        #region 扩展属性
        public object EntityId { get => BrandsId; }
        #endregion
    }
}
