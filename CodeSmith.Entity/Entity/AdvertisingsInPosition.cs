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
    /// Description:sc_advertisings_in_position
    /// </summary>
    [SugarMapping(TableName = "sc_advertisings_in_position")]
    [Serializable]
    public class AdvertisingsInPosition
    {
		#region	构造
		public AdvertisingsInPosition(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static AdvertisingsInPosition New()
        {
                
            AdvertisingsInPosition advertisingsInPosition = new AdvertisingsInPosition();
            advertisingsInPosition.Id = 0;
            advertisingsInPosition.Advertisingid = 0;
            advertisingsInPosition.Positionid = 0;
            return advertisingsInPosition;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 广告位关联标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public int Id { get; set; }
 
        /// <summary>
        /// 广告Id
        /// </summary>
		[SugarMapping(ColumnName = "advertisingid")]
        public int Advertisingid { get; set; }
 
        /// <summary>
        /// 广告位Id
        /// </summary>
		[SugarMapping(ColumnName = "positionid")]
        public int Positionid { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
