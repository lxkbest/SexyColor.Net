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
    /// Description:投票用户关联
    /// </summary>
    [SugarMapping(TableName = "sc_vote_in_user")]
    [Serializable]
    public class VoteInUser
    {
		#region	构造
		public VoteInUser(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static VoteInUser New()
        {
                
            VoteInUser voteInUser = new VoteInUser();
            voteInUser.Id = 0;
            voteInUser.Voteitemid = 0;
            voteInUser.Userid = 0;
            return voteInUser;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 投票用户关联标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 投票项Id
        /// </summary>
		[SugarMapping(ColumnName = "voteitemid")]
        public long Voteitemid { get; set; }
 
        /// <summary>
        /// 用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
