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
    /// Description:权限项目
    /// </summary>
    [SugarMapping(TableName = "sc_permission_items")]
    [Serializable]
    public class PermissionItems
    {
		#region	构造
		public PermissionItems(){
		
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
            permissionItems.IsEnableQuota = false;
            permissionItems.IsEnableScope = false;
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
        /// 是否启用权限额度 0=否、1=是
        /// </summary>
		[SugarMapping(ColumnName = "is_enable_quota")]
        public bool IsEnableQuota { get; set; }
 
        /// <summary>
        /// 是否启用权限范围 0=否、1=是
        /// </summary>
		[SugarMapping(ColumnName = "is_enable_scope")]
        public bool IsEnableScope { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
