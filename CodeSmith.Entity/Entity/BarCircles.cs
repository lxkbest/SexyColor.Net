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
    /// Description:圈子
    /// </summary>
    [SugarMapping(TableName = "sc_bar_circles")]
    [Serializable]
    public class BarCircles
    {
		#region	构造
		public BarCircles(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static BarCircles New()
        {
                
            BarCircles barCircles = new BarCircles();
            barCircles.Circleid = 0;
            barCircles.Ownerid = 0;
            barCircles.CircleCategory = 0;
            barCircles.UserId = 0;
            barCircles.CircleName = string.Empty;
            barCircles.CircleLogo = string.Empty;
            barCircles.Description = string.Empty;
            barCircles.IsEnabled = false;
            barCircles.DisplayOrder = 0;
            barCircles.FollowCount = 0;
            barCircles.Status = false;
            barCircles.DateCreated = DateTime.UtcNow;
            return barCircles;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 圈子Id
        /// </summary>
		[SugarMapping(ColumnName = "circleid")]
        public long Circleid { get; set; }
 
        /// <summary>
        /// 圈子拥有者
        /// </summary>
		[SugarMapping(ColumnName = "ownerid")]
        public long Ownerid { get; set; }
 
        /// <summary>
        /// 圈子分类
        /// </summary>
		[SugarMapping(ColumnName = "circle_category")]
        public int CircleCategory { get; set; }
 
        /// <summary>
        /// 圈主Id
        /// </summary>
		[SugarMapping(ColumnName = "userId")]
        public long UserId { get; set; }
 
        /// <summary>
        /// 圈子名称
        /// </summary>
		[SugarMapping(ColumnName = "circle_name")]
        public string CircleName { get; set; }
 
        /// <summary>
        /// 圈子Logo
        /// </summary>
		[SugarMapping(ColumnName = "circle_logo")]
        public string CircleLogo { get; set; }
 
        /// <summary>
        /// 圈子描述
        /// </summary>
		[SugarMapping(ColumnName = "description")]
        public string Description { get; set; }
 
        /// <summary>
        /// 是否启用
        /// </summary>
		[SugarMapping(ColumnName = "is_enabled")]
        public bool IsEnabled { get; set; }
 
        /// <summary>
        /// 排序序号
        /// </summary>
		[SugarMapping(ColumnName = "display_order")]
        public int DisplayOrder { get; set; }
 
        /// <summary>
        /// 关注数
        /// </summary>
		[SugarMapping(ColumnName = "follow_count")]
        public int FollowCount { get; set; }
 
        /// <summary>
        /// 状态             默认0
        /// </summary>
		[SugarMapping(ColumnName = "status")]
        public bool Status { get; set; }
 
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
