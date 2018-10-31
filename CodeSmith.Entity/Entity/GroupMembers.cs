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
    /// Description:群组成员
    /// </summary>
    [SugarMapping(TableName = "sc_group_members")]
    [Serializable]
    public class GroupMembers
    {
		#region	构造
		public GroupMembers(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static GroupMembers New()
        {
                
            GroupMembers groupMembers = new GroupMembers();
            groupMembers.Id = 0;
            groupMembers.Groupid = 0;
            groupMembers.Userid = 0;
            groupMembers.IsManager = false;
            groupMembers.DateJoin = DateTime.UtcNow;
            return groupMembers;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 群组成员标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 群组Id
        /// </summary>
		[SugarMapping(ColumnName = "groupid")]
        public long Groupid { get; set; }
 
        /// <summary>
        /// 用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 是否群管理    0.否，1.是 默认0
        /// </summary>
		[SugarMapping(ColumnName = "is_manager")]
        public bool IsManager { get; set; }
 
        /// <summary>
        /// 加入时间
        /// </summary>
		[SugarMapping(ColumnName = "date_join")]
        public System.DateTime DateJoin { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
