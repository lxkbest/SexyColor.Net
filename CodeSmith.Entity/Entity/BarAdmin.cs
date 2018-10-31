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
    /// Description:圈管理关联
    /// </summary>
    [SugarMapping(TableName = "sc_bar_admin")]
    [Serializable]
    public class BarAdmin
    {
		#region	构造
		public BarAdmin(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static BarAdmin New()
        {
                
            BarAdmin barAdmin = new BarAdmin();
            barAdmin.Id = 0;
            barAdmin.Circleid = 0;
            barAdmin.Userid = 0;
            barAdmin.DateCreated = DateTime.UtcNow;
            return barAdmin;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 圈管理关联标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 圈子Id
        /// </summary>
		[SugarMapping(ColumnName = "circleid")]
        public long Circleid { get; set; }
 
        /// <summary>
        /// 用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
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
