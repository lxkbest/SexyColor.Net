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
    /// Description:关注用户
    /// </summary>
    [SugarMapping(TableName = "sc_follows")]
    [Serializable]
    public class Follows
    {
		#region	构造
		public Follows(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static Follows New()
        {
                
            Follows follows = new Follows();
            follows.Id = 0;
            follows.Userid = 0;
            follows.FollowedUserid = 0;
            follows.IsMutual = false;
            follows.IsQuietly = false;
            follows.DateCreated = DateTime.UtcNow;
            return follows;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 关注用户标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 关注用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 被关注用户Id
        /// </summary>
		[SugarMapping(ColumnName = "followed_userid")]
        public long FollowedUserid { get; set; }
 
        /// <summary>
        /// 是否相互关注 0=否、1=是
        /// </summary>
		[SugarMapping(ColumnName = "is_mutual")]
        public bool IsMutual { get; set; }
 
        /// <summary>
        /// 是否悄悄关注 0=否、1=是
        /// </summary>
		[SugarMapping(ColumnName = "is_quietly")]
        public bool IsQuietly { get; set; }
 
        /// <summary>
        /// 关注日期
        /// </summary>
		[SugarMapping(ColumnName = "date_created")]
        public System.DateTime DateCreated { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
