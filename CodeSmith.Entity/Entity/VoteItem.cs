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
    /// Description:投票项
    /// </summary>
    [SugarMapping(TableName = "sc_vote_item")]
    [Serializable]
    public class VoteItem
    {
		#region	构造
		public VoteItem(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static VoteItem New()
        {
                
            VoteItem voteItem = new VoteItem();
            voteItem.Voteitemid = 0;
            voteItem.Threadid = 0;
            voteItem.VoteitemName = string.Empty;
            voteItem.VoteitemCount = 0;
            return voteItem;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 投票项Id
        /// </summary>
		[SugarMapping(ColumnName = "voteitemid")]
        public long Voteitemid { get; set; }
 
        /// <summary>
        /// 帖子Id
        /// </summary>
		[SugarMapping(ColumnName = "threadid")]
        public long Threadid { get; set; }
 
        /// <summary>
        /// 投票项名称
        /// </summary>
		[SugarMapping(ColumnName = "voteitem_name")]
        public string VoteitemName { get; set; }
 
        /// <summary>
        /// 获得票数
        /// </summary>
		[SugarMapping(ColumnName = "voteitem_count")]
        public int VoteitemCount { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
