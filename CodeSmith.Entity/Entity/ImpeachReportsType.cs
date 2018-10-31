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
    /// Description:举报类型
    /// </summary>
    [SugarMapping(TableName = "sc_impeach_reports_type")]
    [Serializable]
    public class ImpeachReportsType
    {
		#region	构造
		public ImpeachReportsType(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static ImpeachReportsType New()
        {
                
            ImpeachReportsType impeachReportsType = new ImpeachReportsType();
            impeachReportsType.Id = 0;
            impeachReportsType.Reportsname = string.Empty;
            impeachReportsType.DisplayOrder = 0;
            impeachReportsType.Description = string.Empty;
            return impeachReportsType;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 举报类型标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public int Id { get; set; }
 
        /// <summary>
        /// 举报类型名称
        /// </summary>
		[SugarMapping(ColumnName = "reportsname")]
        public string Reportsname { get; set; }
 
        /// <summary>
        /// 排序序号
        /// </summary>
		[SugarMapping(ColumnName = "display_order")]
        public int DisplayOrder { get; set; }
 
        /// <summary>
        /// 描述
        /// </summary>
		[SugarMapping(ColumnName = "description")]
        public string Description { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
