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
    /// Description:群组成员申请
    /// </summary>
    [SugarMapping(TableName = "sc_groups_members_applies")]
    [Serializable]
    public class GroupsMembersApplies
    {
		#region	构造
		public GroupsMembersApplies(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static GroupsMembersApplies New()
        {
                
            GroupsMembersApplies groupsMembersApplies = new GroupsMembersApplies();
            groupsMembersApplies.Id = 0;
            groupsMembersApplies.Groupid = 0;
            groupsMembersApplies.Userid = 0;
            groupsMembersApplies.ApplyReason = string.Empty;
            groupsMembersApplies.ApplyStatus = false;
            groupsMembersApplies.RejectReason = string.Empty;
            groupsMembersApplies.ApplyDate = DateTime.UtcNow;
            return groupsMembersApplies;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 群组成员申请标识
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
        /// 申请理由
        /// </summary>
		[SugarMapping(ColumnName = "apply_reason")]
        public string ApplyReason { get; set; }
 
        /// <summary>
        /// 申请状态   1.未处理，2.忽略申请，3.已通过申请，4.已拒绝申请
        /// </summary>
		[SugarMapping(ColumnName = "apply_status")]
        public bool ApplyStatus { get; set; }
 
        /// <summary>
        /// 拒绝理由
        /// </summary>
		[SugarMapping(ColumnName = "reject_reason")]
        public string RejectReason { get; set; }
 
        /// <summary>
        /// 申请日期
        /// </summary>
		[SugarMapping(ColumnName = "apply_date")]
        public System.DateTime ApplyDate { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
