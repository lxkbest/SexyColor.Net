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
    /// Description:基础城市
    /// </summary>
    [SugarMapping(TableName = "sc_basics_citys")]
    [Serializable]
    public class BasicsCitys
    {
		#region	构造
		public BasicsCitys(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static BasicsCitys New()
        {
                
            BasicsCitys basicsCitys = new BasicsCitys();
            basicsCitys.Cityid = 0;
            basicsCitys.Provinceid = 0;
            basicsCitys.Cityname = string.Empty;
            return basicsCitys;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 城市Id
        /// </summary>
		[SugarMapping(ColumnName = "cityid")]
        public int Cityid { get; set; }
 
        /// <summary>
        /// 省份Id
        /// </summary>
		[SugarMapping(ColumnName = "provinceid")]
        public int Provinceid { get; set; }
 
        /// <summary>
        /// 城市名称
        /// </summary>
		[SugarMapping(ColumnName = "cityname")]
        public string Cityname { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
