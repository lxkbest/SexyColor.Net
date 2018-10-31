using System;
using MySqlSugar;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:权限角色
    /// </summary>
    [SugarMapping(TableName = "sc_permission_items_in_roles")]
    [CacheSetting(true, PropertyNamesOfArea = "Rolename")]
    [Serializable]
    public class PermissionItemsInRoles : IEntity
    {
        #region	构造
        public PermissionItemsInRoles()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static PermissionItemsInRoles New()
        {
            PermissionItemsInRoles permissionItemsInRoles = new PermissionItemsInRoles();
            permissionItemsInRoles.Id = 0;
            permissionItemsInRoles.Rolename = string.Empty;
            permissionItemsInRoles.Itemkey = string.Empty;
            return permissionItemsInRoles;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 权限角色标识
        /// </summary>
        [SugarMapping(ColumnName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [SugarMapping(ColumnName = "rolename")]
        public string Rolename { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [SugarMapping(ColumnName = "itemkey")]
        public string Itemkey { get; set; }
        #endregion

        #region 扩展属性
        public object EntityId { get => Id; }
        #endregion
    }
}
