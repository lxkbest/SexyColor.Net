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
    /// Description:用户角色
    /// </summary>
    [SugarMapping(TableName = "sc_users_in_roles")]
    [Serializable]
    public class UsersInRoles
    {
		#region	构造
		public UsersInRoles(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static UsersInRoles New()
        {
                
            UsersInRoles usersInRoles = new UsersInRoles();
            usersInRoles.Id = 0;
            usersInRoles.Userid = 0;
            usersInRoles.Rolename = string.Empty;
            return usersInRoles;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 用户角色标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 角色名称
        /// </summary>
		[SugarMapping(ColumnName = "rolename")]
        public string Rolename { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
