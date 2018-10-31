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
    /// Description:用户屏蔽
    /// </summary>
    [SugarMapping(TableName = "sc_user_blocked_objects")]
    [Serializable]
    public class UserBlockedObjects
    {
		#region	构造
		public UserBlockedObjects(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static UserBlockedObjects New()
        {
                
            UserBlockedObjects userBlockedObjects = new UserBlockedObjects();
            userBlockedObjects.Id = 0;
            userBlockedObjects.Userid = 0;
            userBlockedObjects.ObjectType = false;
            userBlockedObjects.ObjectId = 0;
            userBlockedObjects.ObjectName = string.Empty;
            userBlockedObjects.DateCreated = DateTime.UtcNow;
            return userBlockedObjects;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 用户屏蔽标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 被屏蔽对象类型   1.辱骂、2.骚扰、3.威胁
            
        /// </summary>
		[SugarMapping(ColumnName = "object_type")]
        public bool ObjectType { get; set; }
 
        /// <summary>
        /// 被屏蔽对象Id
        /// </summary>
		[SugarMapping(ColumnName = "object_id")]
        public long ObjectId { get; set; }
 
        /// <summary>
        /// 被屏蔽对象名称
        /// </summary>
		[SugarMapping(ColumnName = "object_name")]
        public string ObjectName { get; set; }
 
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
