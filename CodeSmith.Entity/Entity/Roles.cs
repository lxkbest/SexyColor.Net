using System;
using MySqlSugar;

namespace CodeSmith.Entity
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:角色
    /// </summary>
    [SugarMapping(TableName = "sc_roles")]
    [Serializable]
    public class Roles
    {
		#region	构造
		public Roles(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static Roles New()
        {
                
            Roles roles = new Roles();
            roles.Rolename = string.Empty;
            roles.FriendlyRolename = string.Empty;
            roles.IsBuiltin = false;
            roles.ConnectToUser = false;
            roles.IsPublic = false;
            roles.Description = string.Empty;
            roles.RoleImage = string.Empty;
            roles.IsEnabled = false;
            return roles;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 角色名称
        /// </summary>
		[SugarMapping(ColumnName = "rolename")]
        public string Rolename { get; set; }
 
        /// <summary>
        /// 角色友好名称用于对外显示  (可用可不用)
        /// </summary>
		[SugarMapping(ColumnName = "friendly_rolename")]
        public string FriendlyRolename { get; set; }
 
        /// <summary>
        /// 是否系统内置默认  0：否、1：是
        /// </summary>
		[SugarMapping(ColumnName = "is_builtin")]
        public bool IsBuiltin { get; set; }
 
        /// <summary>
        /// 是否直接关联到用户  0：否、1：是
        /// </summary>
		[SugarMapping(ColumnName = "connect_to_user")]
        public bool ConnectToUser { get; set; }
 
        /// <summary>
        /// 是否对外显示 0：否、1：是
        /// </summary>
		[SugarMapping(ColumnName = "is_public")]
        public bool IsPublic { get; set; }
 
        /// <summary>
        /// 描述
        /// </summary>
		[SugarMapping(ColumnName = "description")]
        public string Description { get; set; }
 
        /// <summary>
        /// 角色标识图片
        /// </summary>
		[SugarMapping(ColumnName = "role_image")]
        public string RoleImage { get; set; }
 
        /// <summary>
        /// 是否启用 0：否、1：是
        /// </summary>
		[SugarMapping(ColumnName = "is_enabled")]
        public bool IsEnabled { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
