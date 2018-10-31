using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySqlSugar;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
	/// <summary>
    /// 基础省份
    /// </summary>
    [SugarMapping(TableName = "sc_basics_provinces")]
    [Serializable]
    public class BasicsProvinces:IEntity
    {
		#region	构造
		public BasicsProvinces(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static BasicsProvinces New()
        {

            BasicsProvinces basicsProvinces = new BasicsProvinces();
            basicsProvinces.Provinceid = 0;
            basicsProvinces.Provincename = string.Empty;
            return basicsProvinces;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 省份标识
        /// </summary>
		[SugarMapping(ColumnName = "provinceid")]
        public int Provinceid { get; set; }
 
        /// <summary>
        /// 省份名称
        /// </summary>
		[SugarMapping(ColumnName = "provincename")]
        public string Provincename { get; set; }

        #endregion

        #region 扩展属性
        public object EntityId { get => Provinceid; }
        #endregion
    }
}
