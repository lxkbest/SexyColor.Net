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
    /// Description:圈子分类
    /// </summary>
    [SugarMapping(TableName = "sc_bar_categorys")]
    [Serializable]
    public class BarCategorys
    {
		#region	构造
		public BarCategorys(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static BarCategorys New()
        {
                
            BarCategorys barCategorys = new BarCategorys();
            barCategorys.Id = 0;
            barCategorys.Name = string.Empty;
            return barCategorys;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 圈子分类标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public int Id { get; set; }
 
        /// <summary>
        /// 圈子分类名称
        /// </summary>
		[SugarMapping(ColumnName = "name")]
        public string Name { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
