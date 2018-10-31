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
    /// Description:sc_try_sexy
    /// </summary>
    [SugarMapping(TableName = "sc_try_sexy")]
    [Serializable]
    public class TrySexy
    {
		#region	构造
		public TrySexy(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static TrySexy New()
        {
                
            TrySexy trySexy = new TrySexy();
            trySexy.Activityid = 0;
            trySexy.ActivityName = string.Empty;
            trySexy.ActivityType = false;
            trySexy.ApplyEndDate = DateTime.UtcNow;
            trySexy.StartDate = DateTime.UtcNow;
            trySexy.EndDate = DateTime.UtcNow;
            trySexy.ApplyMamberCount = 0;
            trySexy.ExperiencePrice = decimal.Zero;
            trySexy.ExperienceCount = 0;
            trySexy.States = 0;
            trySexy.DateCreated = DateTime.UtcNow;
            return trySexy;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 活动Id
        /// </summary>
		[SugarMapping(ColumnName = "activityid")]
        public long Activityid { get; set; }
 
        /// <summary>
        /// 活动名称
        /// </summary>
		[SugarMapping(ColumnName = "activity_name")]
        public string ActivityName { get; set; }
 
        /// <summary>
        /// 活动类型   暂时没想好留着先
        /// </summary>
		[SugarMapping(ColumnName = "activity_type")]
        public bool ActivityType { get; set; }
 
        /// <summary>
        /// 活动申请截止日期
        /// </summary>
		[SugarMapping(ColumnName = "apply_end_date")]
        public System.DateTime ApplyEndDate { get; set; }
 
        /// <summary>
        /// 活动开始日期
        /// </summary>
		[SugarMapping(ColumnName = "start_date")]
        public System.DateTime StartDate { get; set; }
 
        /// <summary>
        /// 活动结束日期
        /// </summary>
		[SugarMapping(ColumnName = "end_date")]
        public System.DateTime EndDate { get; set; }
 
        /// <summary>
        /// 申请人数
        /// </summary>
		[SugarMapping(ColumnName = "apply_mamber_count")]
        public int ApplyMamberCount { get; set; }
 
        /// <summary>
        /// 试用品价值
        /// </summary>
		[SugarMapping(ColumnName = "experience_price")]
        public decimal ExperiencePrice { get; set; }
 
        /// <summary>
        /// 试用品数量
        /// </summary>
		[SugarMapping(ColumnName = "experience_count")]
        public int ExperienceCount { get; set; }
 
        /// <summary>
        /// 状态   1.开始报名，2.报名结束，3.活动结束
        /// </summary>
		[SugarMapping(ColumnName = "states")]
        public int States { get; set; }
 
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
