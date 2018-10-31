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
    /// Description:系统全局设置
    /// </summary>
    [SugarMapping(TableName = "sc_global_settings")]
    [Serializable]
    public class GlobalSettings
    {
		#region	构造
		public GlobalSettings(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static GlobalSettings New()
        {
                
            GlobalSettings globalSettings = new GlobalSettings();
            globalSettings.GlobalFreightSetting = 0;
            globalSettings.GlobalReturnGoodsSetting = 0;
            globalSettings.GlobalOrderSetting = 0;
            globalSettings.GlobalFreeMoneySetting = decimal.Zero;
            return globalSettings;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 全局运费设置
        /// </summary>
		[SugarMapping(ColumnName = "global_freight_setting")]
        public int GlobalFreightSetting { get; set; }
 
        /// <summary>
        /// 全局退货期限设置
        /// </summary>
		[SugarMapping(ColumnName = "global_return_goods_setting")]
        public int GlobalReturnGoodsSetting { get; set; }
 
        /// <summary>
        /// 全局下单有效时间
        /// </summary>
		[SugarMapping(ColumnName = "global_order_setting")]
        public int GlobalOrderSetting { get; set; }
 
        /// <summary>
        /// 全局免运费达到金额
        /// </summary>
		[SugarMapping(ColumnName = "global_free_money_setting")]
        public decimal GlobalFreeMoneySetting { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
