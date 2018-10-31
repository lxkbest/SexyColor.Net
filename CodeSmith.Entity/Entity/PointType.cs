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
    /// Description:积分类型
    /// </summary>
    [SugarMapping(TableName = "sc_point_type")]
    [Serializable]
    public class PointType
    {
		#region	构造
		public PointType(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static PointType New()
        {
                
            PointType pointType = new PointType();
            pointType.Typekey = string.Empty;
            pointType.Typename = string.Empty;
            pointType.QuotaDay = 0;
            pointType.DisplayOrder = 0;
            pointType.Description = string.Empty;
            return pointType;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 积分类型标识
        /// </summary>
		[SugarMapping(ColumnName = "typekey")]
        public string Typekey { get; set; }
 
        /// <summary>
        /// 积分类型名称
        /// </summary>
		[SugarMapping(ColumnName = "typename")]
        public string Typename { get; set; }
 
        /// <summary>
        /// 限额    每人每日该类限额（0表示无限制）
        /// </summary>
		[SugarMapping(ColumnName = "quota_day")]
        public int QuotaDay { get; set; }
 
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
