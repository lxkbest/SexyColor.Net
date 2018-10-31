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
    /// Description:物流公司
    /// </summary>
    [SugarMapping(TableName = "sc_logistics_company")]
    [Serializable]
    public class LogisticsCompany
    {
		#region	构造
		public LogisticsCompany(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static LogisticsCompany New()
        {
                
            LogisticsCompany logisticsCompany = new LogisticsCompany();
            logisticsCompany.Id = 0;
            logisticsCompany.LogisticsName = string.Empty;
            logisticsCompany.LogisticsFirstPrice = decimal.Zero;
            logisticsCompany.LogisticsNextPrice = decimal.Zero;
            logisticsCompany.Remarks = string.Empty;
            return logisticsCompany;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 物流公司标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public int Id { get; set; }
 
        /// <summary>
        /// 物流公司名称
        /// </summary>
		[SugarMapping(ColumnName = "logistics_name")]
        public string LogisticsName { get; set; }
 
        /// <summary>
        /// 首重运费金额
        /// </summary>
		[SugarMapping(ColumnName = "logistics_first_price")]
        public decimal LogisticsFirstPrice { get; set; }
 
        /// <summary>
        /// 续重运费金额
        /// </summary>
		[SugarMapping(ColumnName = "logistics_next_price")]
        public decimal LogisticsNextPrice { get; set; }
 
        /// <summary>
        /// 备注
        /// </summary>
		[SugarMapping(ColumnName = "remarks")]
        public string Remarks { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
