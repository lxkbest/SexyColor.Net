using System;
using System.ComponentModel.DataAnnotations;

namespace SexyColor.Web
{
    public class BannedUserModel
    {
        /// <summary>
        /// 截止日期
        /// </summary>
        [Required(ErrorMessage = "请输入截止日期")]
        [DataType(DataType.DateTime, ErrorMessage = "截止日期必须为时间类型")]
        public DateTime BanDeadline { get; set; }

        /// <summary>
        /// 封禁原因
        /// </summary>
        [Required(ErrorMessage = "请输入封禁原因")]
        [StringLength(64, ErrorMessage = "最多输入64个字符")]
        public string BanReason { get; set; }
    }
}
