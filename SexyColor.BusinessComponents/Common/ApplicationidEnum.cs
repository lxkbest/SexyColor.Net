using System.ComponentModel.DataAnnotations;

namespace SexyColor.BusinessComponents
{
    public enum ApplicationidEnum
    {
        /// <summary>
        /// 后台系统应用
        /// </summary>
        [Display(Name = "后台系统应用")]
        OnlineMall = 1001,

        /// <summary>
        /// WebApi应用
        /// </summary>
        [Display(Name = "WebApi应用")]
        WebApi = 1002,

        /// <summary>
        /// 圈子应用
        /// </summary>
        [Display(Name = "圈子应用")]
        Bar = 1003,

        /// <summary>
        /// 微博应用
        /// </summary>
        [Display(Name = "微博应用")]
        Weibo = 1004,

        /// <summary>
        /// 群组消息应用
        /// </summary>
        [Display(Name = "群组消息应用")]
        Groups = 1005
    }
}
