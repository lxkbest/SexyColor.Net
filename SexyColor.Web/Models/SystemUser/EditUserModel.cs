using SexyColor.CommonComponents;
using System;
using System.ComponentModel.DataAnnotations;

namespace SexyColor.Web
{
    public class EditUserModel
    {
        /// <summary>
        ///  用户Id
        /// </summary>
        [Display(Name = "用户Id")]
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码问题
        /// </summary>
        [Display(Name = "密码问题")]
        [StringLength(64, ErrorMessage = "密码问题最大长度为64个字符")]
        [Required(ErrorMessage = "密码问题为必填选项")]
        [DataType(DataType.Text)]
        public string PasswordQuestion { get; set; }

        /// <summary>
        /// 密码答案
        /// </summary>
        [Display(Name = "密码答案")]
        [StringLength(64, ErrorMessage = "密码答案最大长度为64个字符")]
        [Required(ErrorMessage = "密码答案为必填选项")]
        [DataType(DataType.Text)]
        public string PasswordAnswer { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Display(Name = "昵称")]
        [Required(ErrorMessage = "请输入昵称")]
        [DataType(DataType.Text)]
        public string NickName { get; set; }

        /// <summary>
        /// 帐号邮箱
        /// </summary>
        [Display(Name = "帐号邮箱")]
        [StringLength(64, ErrorMessage = "邮箱最大长度为64个字符")]
        public string AccountEmail { get; set; }

        /// <summary>
        /// 帐号邮箱是否通过验证
        /// </summary>
        [Display(Name = "帐号邮箱是否通过验证")]
        public bool IsEmailVerified { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "手机号码为必填选项")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "手机号码只允许为11个字符")]
        public string AccountMobile { get; set; }

        /// <summary>
        /// 帐号手机是否通过验证
        /// </summary>
        [Display(Name = "帐号手机是否通过验证")]
        public bool IsMobileVerified { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Required(ErrorMessage = "请输入创建时间")]
        [DataType(DataType.DateTime, ErrorMessage = "创建时间必须为时间类型")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 是否强制用户登录
        /// </summary>

        [Display(Name = "是否强制用户登录")]
        public bool ForceLogin { get; set; }

        /// <summary>
        /// 创建用户时的IP
        /// </summary>
        [Display(Name = "创建用户时的IP")]
        public string IpCreated { get; set; }

        /// <summary>
        /// 用户类别
        /// </summary>
        [Display(Name ="用户类别")]
        public int UserType { get; set; }

        /// <summary>
        /// 上次活动时间
        /// </summary>
        [Display(Name = "上次活动时间")]
        public DateTime LastActivityTime { get; set; }

        /// <summary>
        /// 上次操作
        /// </summary>
        [Display(Name = "上次操作")]
        public string LastAction { get; set; }

        /// <summary>
        /// 上次活动时的IP
        /// </summary>
        [Display(Name = "上次活动时的IP")]
        public string IpLastActivity { get; set; }

        /// <summary>
        /// 封禁截止日期
        /// </summary>
        [Display(Name = "封禁截止日期")]
        public DateTime BanDeadLine { get; set; }

        /// <summary>
        /// 强制用户管制（不会自动解除）
        /// </summary>
        [Display(Name = "强制用户管制")]
        public bool IsForceModerated { get; set; }


        /// <summary>
        /// 关注用户数
        /// </summary>
        [Display(Name = "关注用户数")]
        [Required(ErrorMessage = "关注用户数为必填选项")]
        [IsPositiveIntegerType(ErrorMessage = "关注用户数只允许为正整数")]
        public int FollowedCount { get; set; }

        /// <summary>
        /// 粉丝数
        /// </summary>
        [Display(Name = "粉丝数")]
        [Required(ErrorMessage = "粉丝数为必填选项")]
        [IsPositiveIntegerType(ErrorMessage = "粉丝数只允许为正整数")]
        public int FollowerCount { get; set; }

        /// <summary>
        /// 用户等级
        /// </summary>
        [Display(Name = "用户等级")]
        [Required(ErrorMessage = "用户等级为必填选项")]
        [IsPositiveIntegerType(ErrorMessage = "用户等级只允许为正整数")]
        public int Rank { get; set; }

        /// <summary>
        /// 积分值
        /// </summary>
        [Display(Name = "积分值")]
        [Required(ErrorMessage = "积分值为必填选项")]
        [IsPositiveIntegerType(ErrorMessage = "积分值只允许为正整数")]
        public int SexyPoints { get; set; }

        /// <summary>
        /// 冻结的积分值
        /// </summary>
        [Display(Name = "冻结的积分值")]
        [Required(ErrorMessage = "冻结的积分值为必填选项")]
        [IsPositiveIntegerType(ErrorMessage = "冻结的积分值只允许为正整数")]
        public int FrozenSexyPoints { get; set; }

        /// <summary>
        /// 是否通过认证
        /// </summary>
        [Display(Name = "是否通过认证")]
        public bool IsAuthentication { get; set; }

    }
}
