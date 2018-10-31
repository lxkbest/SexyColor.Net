namespace SexyColor.BusinessComponents
{
    public enum UserCreateStatus
    {
        /// <summary>
        /// 未知错误
        /// </summary>
        UnknownFailure = 0,

        /// <summary>
        /// 创建成功
        /// </summary>
        Created = 1,

        /// <summary>
        /// 用户名重复
        /// </summary>
        DuplicateUsername = 2,

        /// <summary>
        /// Email重复
        /// </summary>
        DuplicateEmailAddress = 3,

        /// <summary>
        /// 手机号重复
        /// </summary>
        DuplicateMobile = 4,


        /// <summary>
        /// 不允许的用户名
        /// </summary>
        DisallowedUsername = 5,

        /// <summary>
        /// 更新成功
        /// </summary>
        Updated = 6,

        /// <summary>
        /// 不合法的密码提示问题/答案
        /// </summary>
        InvalidQuestionAnswer = 7,

        /// <summary>
        /// 不合法的密码
        /// </summary>
        InvalidPassword = 8
    }

    /// <summary>
    /// 用户登录状态
    /// </summary>
    public enum UserLoginStatus
    {
        /// <summary>
        /// 通过身份验证，登录成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 用户名、密码不匹配
        /// </summary>
        InvalidCredentials = 1,

        /// <summary>
        /// 帐户未激活
        /// </summary>
        NotActivated = 2,

        /// <summary>
        /// 帐户被封禁
        /// </summary>
        Banned = 3,

        /// <summary>
        /// 验证码不匹配
        /// </summary>
        CapCode = 4,

        /// <summary>
        /// 未知错误
        /// </summary>
        UnknownError = 100

        
    }

    /// <summary>
    /// 用户密码存储格式
    /// </summary>
    public enum UserPasswordFormat
    {
        /// <summary>
        /// 密码未加密
        /// </summary>
        Clear = 0,

        /// <summary>
        /// 标准MD5加密
        /// </summary>
        MD5 = 1,
    }

    /// <summary>
    /// 用户资料完整度有关项目
    /// </summary>
    public enum ProfileIntegrityItems
    {
        /// <summary>
        /// 头像
        /// </summary>
        Headimg = 0,

        /// <summary>
        /// 生日
        /// </summary>
        Birthday = 1,

        /// <summary>
        /// 年龄
        /// </summary>
        Age = 2,
        
        /// <summary>
        /// 性别
        /// </summary>
        Sex = 3,

        /// <summary>
        /// 性取向
        /// </summary>
        SexualOrientation = 4,

        /// <summary>
        /// 婚姻状况
        /// </summary>
        Sarriage = 5,

        /// <summary>
        /// 所在地
        /// </summary>
        NowArea = 6,

    }
}
