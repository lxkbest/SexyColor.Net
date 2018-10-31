using System;
using MySqlSugar;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:权限项目
    /// </summary>
    [SugarMapping(TableName = "sc_permission_items")]
    [CacheSetting(true)]
    [Serializable]
    public class PermissionItems : IEntity
    {
        #region	构造
        public PermissionItems()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static PermissionItems New()
        {
            PermissionItems permissionItems = new PermissionItems();
            permissionItems.Itemkey = string.Empty;
            permissionItems.Applicationid = 0;
            permissionItems.Itemname = string.Empty;
            permissionItems.DisplayOrder = 0;
            permissionItems.EventName = string.Empty;
            permissionItems.IsEnable = true;
            permissionItems.IsNewAction = true;
            permissionItems.ItemType = 0;
            permissionItems.DateCreated = DateTime.Now;
            return permissionItems;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 权限key
        /// </summary>
        [SugarMapping(ColumnName = "itemkey")]
        public string Itemkey { get; set; }

        /// <summary>
        /// 模块Id   (后期使用)
        /// </summary>
        [SugarMapping(ColumnName = "applicationid")]
        public int Applicationid { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [SugarMapping(ColumnName = "itemname")]
        public string Itemname { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [SugarMapping(ColumnName = "display_order")]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 权限父级
        /// </summary>
        [SugarMapping(ColumnName = "parentsid")]
        public string Parentsid { get; set; }

        /// <summary>
        /// 权限Url
        /// </summary>
        [SugarMapping(ColumnName = "item_url")]
        public string ItemUrl { get; set; }

        /// <summary>
        /// 事件或者按钮名
        /// </summary>
        [SugarMapping(ColumnName = "event_name")]
        public string EventName { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        [SugarMapping(ColumnName = "item_type")]
        public int ItemType { get; set; }

        /// <summary>
        /// 是否新功能
        /// </summary>
        [SugarMapping(ColumnName = "is_new_action")]
        public bool IsNewAction { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarMapping(ColumnName = "date_created")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [SugarMapping(ColumnName = "is_enable")]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 上次更新时间
        /// </summary>
        [SugarMapping(ColumnName = "date_last_modified")]
        public DateTime DateLastModified { get; set; }

        #endregion

        #region 扩展属性
        public object EntityId { get => Itemkey; }
        public bool IsHave { get; set; }
        #endregion
    }
}
