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
    /// Description:基础区域
    /// </summary>
    [SugarMapping(TableName = "sc_basics_areas")]
    [Serializable]
    public class BasicsAreas
    {
		#region	构造
		public BasicsAreas(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static BasicsAreas New()
        {
                
            BasicsAreas basicsAreas = new BasicsAreas();
            basicsAreas.Areaid = 0;
            basicsAreas.Cityid = 0;
            basicsAreas.Areaname = string.Empty;
            return basicsAreas;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 区域Id
        /// </summary>
		[SugarMapping(ColumnName = "areaid")]
        public int Areaid { get; set; }
 
        /// <summary>
        /// 城市Id
        /// </summary>
		[SugarMapping(ColumnName = "cityid")]
        public int Cityid { get; set; }
 
        /// <summary>
        /// 区域名称
        /// </summary>
		[SugarMapping(ColumnName = "areaname")]
        public string Areaname { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
