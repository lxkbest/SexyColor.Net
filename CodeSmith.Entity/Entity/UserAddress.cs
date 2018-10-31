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
    /// Description:用户收货地址
    /// </summary>
    [SugarMapping(TableName = "sc_user_address")]
    [Serializable]
    public class UserAddress
    {
		#region	构造
		public UserAddress(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static UserAddress New()
        {
                
            UserAddress userAddress = new UserAddress();
            userAddress.Id = 0;
            userAddress.Userid = 0;
            userAddress.Name = string.Empty;
            userAddress.Phone = string.Empty;
            userAddress.Address = string.Empty;
            userAddress.IsDefault = false;
            return userAddress;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 用户地址标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public int Id { get; set; }
 
        /// <summary>
        /// 用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 收件人
        /// </summary>
		[SugarMapping(ColumnName = "name")]
        public string Name { get; set; }
 
        /// <summary>
        /// 手机号码
        /// </summary>
		[SugarMapping(ColumnName = "phone")]
        public string Phone { get; set; }
 
        /// <summary>
        /// 收件地址
        /// </summary>
		[SugarMapping(ColumnName = "address")]
        public string Address { get; set; }
 
        /// <summary>
        /// 是否默认地址  0: 否，1:是
        /// </summary>
		[SugarMapping(ColumnName = "is_default")]
        public bool IsDefault { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
