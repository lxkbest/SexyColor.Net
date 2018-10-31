﻿using System;
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
    /// Description:用户等级
    /// </summary>
    [SugarMapping(TableName = "sc_user_ranks")]
    [Serializable]
    public class UserRanks
    {
		#region	构造
		public UserRanks(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static UserRanks New()
        {
                
            UserRanks userRanks = new UserRanks();
            userRanks.Rank = 0;
            userRanks.RankName = string.Empty;
            userRanks.PointLower = 0;
            return userRanks;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 级别  从1级开始
        /// </summary>
		[SugarMapping(ColumnName = "rank")]
        public int Rank { get; set; }
 
        /// <summary>
        /// 级别名称
        /// </summary>
		[SugarMapping(ColumnName = "rank_name")]
        public string RankName { get; set; }
 
        /// <summary>
        /// 经验值下限    （表示：某个等级最低要求多少经验）
        /// </summary>
		[SugarMapping(ColumnName = "point_lower")]
        public int PointLower { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
