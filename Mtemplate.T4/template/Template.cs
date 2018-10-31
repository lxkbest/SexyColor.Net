

using System;

namespace Test
{
    /// <summary>
	/// Author:梁雄开
    /// CreatDate:2017-04-05 
    /// Description:sc_goods_sku_info 
    /// </summary>    
    [Serializable]
    public class goods_sku_info
    {
                
		/// <summary>
        /// 商品规格Id
        /// </summary>
        public long skuid { get; set; }
		        
		/// <summary>
        /// 商品Id
        /// </summary>
        public long goodsid { get; set; }
		        
		/// <summary>
        /// 商品规格名称
        /// </summary>
        public string sku_name { get; set; }
		        
		/// <summary>
        /// 商品规格进货价
        /// </summary>
        public decimal sku_original_price { get; set; }
		        
		/// <summary>
        /// 商品规格市场价
        /// </summary>
        public decimal sku_maket_price { get; set; }
		        
		/// <summary>
        /// 商品规格销售价
        /// </summary>
        public decimal sku_factory_price { get; set; }
		        
		/// <summary>
        /// 商品规格会员价
        /// </summary>
        public decimal sku_vip_price { get; set; }
		        
		/// <summary>
        /// 库存
        /// </summary>
        public int stock { get; set; }
		    }
}
	
namespace Test
{
    /// <summary>
	/// Author:梁雄开
    /// CreatDate:2017-04-05 
    /// Description:sc_thread_browse_logs 
    /// </summary>    
    [Serializable]
    public class thread_browse_logs
    {
                
		/// <summary>
        /// 帖子浏览记录标识
        /// </summary>
        public long id { get; set; }
		        
		/// <summary>
        /// 圈子Id
        /// </summary>
        public long circleid { get; set; }
		        
		/// <summary>
        /// 帖子Id
        /// </summary>
        public long threadid { get; set; }
		        
		/// <summary>
        /// 用户Id
        /// </summary>
        public long userid { get; set; }
		        
		/// <summary>
        /// 帖子标题
        /// </summary>
        public string thread_subject { get; set; }
		        
		/// <summary>
        /// 圈子名称
        /// </summary>
        public string circle_name { get; set; }
		        
		/// <summary>
        /// 创建时间
        /// </summary>
        public DateTime date_created { get; set; }
		    }
}
	
namespace Test
{
    /// <summary>
	/// Author:梁雄开
    /// CreatDate:2017-04-05 
    /// Description:sc_user 
    /// </summary>    
    [Serializable]
    public class user
    {
                
		/// <summary>
        /// 用户Id
        /// </summary>
        public long userid { get; set; }
		        
		/// <summary>
        /// 用户OpenId
        /// </summary>
        public string openid { get; set; }
		        
		/// <summary>
        /// 用户名    创建唯一索引
        /// </summary>
        public string username { get; set; }
		        
		/// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
		        
		/// <summary>
        /// 加密方式 1=Clear（明文）、2=标准MD5、3.其他
        /// </summary>
        public int password_format { get; set; }
		        
		/// <summary>
        /// 密码问题
        /// </summary>
        public string password_question { get; set; }
		        
		/// <summary>
        /// 密码答案
        /// </summary>
        public string password_answer { get; set; }
		        
		/// <summary>
        /// 帐号邮箱    (增加索引)
        /// </summary>
        public string account_email { get; set; }
		        
		/// <summary>
        /// 帐号邮箱是否通过验证 0=否、1=是
        /// </summary>
        public int is_email_verified { get; set; }
		        
		/// <summary>
        /// 手机号码  增加索引
        /// </summary>
        public string account_mobile { get; set; }
		        
		/// <summary>
        /// 帐号手机是否通过验证  0=否、1=是
        /// </summary>
        public int is_mobile_verified { get; set; }
		        
		/// <summary>
        /// 昵称
        /// </summary>
        public string nickname { get; set; }
		        
		/// <summary>
        /// 是否强制用户登录 0=否、1=是
        /// </summary>
        public int force_login { get; set; }
		        
		/// <summary>
        /// 帐户是否激活 0=否、1=是
        /// </summary>
        public int is_activated { get; set; }
		        
		/// <summary>
        /// 创建时间
        /// </summary>
        public DateTime date_created { get; set; }
		        
		/// <summary>
        /// 创建用户时的IP
        /// </summary>
        public string ip_created { get; set; }
		        
		/// <summary>
        /// 用户类别 1=普通用户、2=管理用户
        /// </summary>
        public int user_type { get; set; }
		        
		/// <summary>
        /// 上次活动时间
        /// </summary>
        public DateTime last_activity_time { get; set; }
		        
		/// <summary>
        /// 上次操作
        /// </summary>
        public string last_action { get; set; }
		        
		/// <summary>
        /// 上次活动时的IP
        /// </summary>
        public string ip_last_activity { get; set; }
		        
		/// <summary>
        /// 是否封禁 0=否、1=是
        /// </summary>
        public int is_banned { get; set; }
		        
		/// <summary>
        /// 封禁原因
        /// </summary>
        public string ban_reason { get; set; }
		        
		/// <summary>
        /// 封禁截止日期
        /// </summary>
        public DateTime ban_dead_line { get; set; }
		        
		/// <summary>
        /// 用户是否被管制   0=否、1=是
        /// </summary>
        public int is_moderated { get; set; }
		        
		/// <summary>
        /// 强制用户管制    0=否、1=是
        /// </summary>
        public int is_force_moderated { get; set; }
		        
		/// <summary>
        /// 头像
        /// </summary>
        public string head_img { get; set; }
		        
		/// <summary>
        /// 关注用户数 增加索引
        /// </summary>
        public int followed_count { get; set; }
		        
		/// <summary>
        /// 粉丝数 增加索引
        /// </summary>
        public int follower_count { get; set; }
		        
		/// <summary>
        /// 用户等级
        /// </summary>
        public int rank { get; set; }
		        
		/// <summary>
        /// 积分值
        /// </summary>
        public int sexy_points { get; set; }
		        
		/// <summary>
        /// 冻结的积分值
        /// </summary>
        public int frozen_sexy_points { get; set; }
		        
		/// <summary>
        /// 是否通过认证
        /// </summary>
        public int is_authentication { get; set; }
		    }
}
	 