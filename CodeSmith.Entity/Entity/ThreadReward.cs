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
    /// Description:帖子打赏
    /// </summary>
    [SugarMapping(TableName = "sc_thread_reward")]
    [Serializable]
    public class ThreadReward
    {
		#region	构造
		public ThreadReward(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static ThreadReward New()
        {
                
            ThreadReward threadReward = new ThreadReward();
            threadReward.Id = 0;
            threadReward.Threadid = 0;
            threadReward.Userid = 0;
            threadReward.UserName = string.Empty;
            threadReward.RewardSexyPoints = 0;
            threadReward.ExperiencePoints = 0;
            threadReward.DateCreated = DateTime.UtcNow;
            return threadReward;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 帖子打赏标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 打赏帖子Id
        /// </summary>
		[SugarMapping(ColumnName = "threadid")]
        public long Threadid { get; set; }
 
        /// <summary>
        /// 打赏用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 打赏用户名
        /// </summary>
		[SugarMapping(ColumnName = "user_name")]
        public string UserName { get; set; }
 
        /// <summary>
        /// 打赏积分值
        /// </summary>
		[SugarMapping(ColumnName = "reward_sexy_points")]
        public int RewardSexyPoints { get; set; }
 
        /// <summary>
        /// 获得经验值
        /// </summary>
		[SugarMapping(ColumnName = "experience_points")]
        public int ExperiencePoints { get; set; }
 
        /// <summary>
        /// 创建时间
        /// </summary>
		[SugarMapping(ColumnName = "date_created")]
        public System.DateTime DateCreated { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
