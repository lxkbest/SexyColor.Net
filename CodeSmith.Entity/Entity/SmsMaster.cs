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
    /// Description:短信模板
    /// </summary>
    [SugarMapping(TableName = "sc_sms_master")]
    [Serializable]
    public class SmsMaster
    {
		#region	构造
		public SmsMaster(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static SmsMaster New()
        {
                
            SmsMaster smsMaster = new SmsMaster();
            smsMaster.Id = 0;
            smsMaster.MasterName = string.Empty;
            smsMaster.MasterContent = string.Empty;
            smsMaster.MasterType = false;
            smsMaster.DeteCreated = DateTime.UtcNow;
            smsMaster.Remarks = string.Empty;
            return smsMaster;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 短信模板标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public int Id { get; set; }
 
        /// <summary>
        /// 模板名称
        /// </summary>
		[SugarMapping(ColumnName = "master_name")]
        public string MasterName { get; set; }
 
        /// <summary>
        /// 模版内容
        /// </summary>
		[SugarMapping(ColumnName = "master_content")]
        public string MasterContent { get; set; }
 
        /// <summary>
        /// 模版类型  1.系统处理模板，2.系统验证码模板，3.问候模板等等
        /// </summary>
		[SugarMapping(ColumnName = "master_type")]
        public bool MasterType { get; set; }
 
        /// <summary>
        /// 创建时间
        /// </summary>
		[SugarMapping(ColumnName = "dete_created")]
        public System.DateTime DeteCreated { get; set; }
 
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
