using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    [Serializable]
    public class PermissionItemsInRoles
    {
		#region	构造
		public PermissionItemsInRoles(){
		
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
            permissionItemsInRoles.PermissionType = 0;
            permissionItemsInRoles.PermissionQuota = 
            permissionItemsInRoles.PermissionScope = 0;
            permissionItemsInRoles.IsLocked = false;
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
 
        /// <summary>
        /// 权限设置类型 1.功能2.菜单3.模块4.应用
        /// </summary>
		[SugarMapping(ColumnName = "permission_type")]
        public int PermissionType { get; set; }
 
        /// <summary>
        /// 允许权限的额度
        /// </summary>
		[SugarMapping(ColumnName = "permission_quota")]
        public float PermissionQuota { get; set; }
 
        /// <summary>
        /// 允许权限的范围
        /// </summary>
		[SugarMapping(ColumnName = "permission_scope")]
        public int PermissionScope { get; set; }
 
        /// <summary>
        /// 是否锁定 0=否、1=是
        /// </summary>
		[SugarMapping(ColumnName = "is_locked")]
        public bool IsLocked { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
