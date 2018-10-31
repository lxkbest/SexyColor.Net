using MySqlSugar;
using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.BusinessComponents
{
    [CacheSetting(true, ExpirationPolicy = CachingExpirationType.Stable)]
    [SugarMapping(TableName = "sc_category_item_chara")]
    [Serializable]
    public class CategoryItemChara : IEntity
    {
        #region	构造
        public CategoryItemChara()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static CategoryItemChara New()
        {
            CategoryItemChara categoryItemChara = new CategoryItemChara();
            categoryItemChara.CharaId = 0;
            categoryItemChara.CharaName = string.Empty;
            categoryItemChara.DeteCreated = DateTime.UtcNow;
            return categoryItemChara;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 特征Id
        /// </summary>
        public int CharaId { get; set; }

        /// <summary>
        /// 特征名称
        /// </summary>
        [SugarMapping(ColumnName = "chara_name")]
        public string CharaName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarMapping(ColumnName = "dete_created")]
        public DateTime DeteCreated { get; set; }
        #endregion

        #region 扩展属性
        public object EntityId { get => CharaId; }
        #endregion
    }
}
