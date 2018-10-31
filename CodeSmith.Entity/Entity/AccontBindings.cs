using System;
using MySqlSugar;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:第三方账号绑定
    /// </summary>
    [SugarMapping(TableName = "sc_accont_bindings")]
    [Serializable]
    public class AccontBindings
    {
		#region	构造
		public AccontBindings(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static AccontBindings New()
        {
                
            AccontBindings accontBindings = new AccontBindings();
            accontBindings.Id = 0;
            accontBindings.Userid = 0;
            accontBindings.AccountTypeKey = string.Empty;
            accontBindings.IdentificaTion = string.Empty;
            accontBindings.AccessToken = string.Empty;
            return accontBindings;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 第三方账号绑定标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 用户Id   索引
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 第三方账号类型  增加索引
        /// </summary>
		[SugarMapping(ColumnName = "account_type_key")]
        public string AccountTypeKey { get; set; }
 
        /// <summary>
        /// 第三方账号标识   增加索引
        /// </summary>
		[SugarMapping(ColumnName = "identifica_tion")]
        public string IdentificaTion { get; set; }
 
        /// <summary>
        /// OAuth授权凭证加密串
        /// </summary>
		[SugarMapping(ColumnName = "access_token")]
        public string AccessToken { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
