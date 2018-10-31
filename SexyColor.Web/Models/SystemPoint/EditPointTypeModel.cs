using System.ComponentModel.DataAnnotations;

namespace SexyColor.Web
{
    public class EditPointTypeModel
    {
        /// <summary>
        /// 积分类型标识
        /// </summary>
        [Display(Name = "积分类型标识")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public string Typekey { get; set; }

        /// <summary>
        /// 积分类型名称
        /// </summary>
        [Display(Name = "积分类型名称")]
        [Required(ErrorMessage = "{0}为必填选项")]
        [StringLength(32,ErrorMessage ="{0}字符长度最大为{1}个字符")]
        public string Typename { get; set; }

        /// <summary>
        /// 限额    每人每日该类限额（0表示无限制）
        /// </summary>
        [Display(Name = "限额")]
        [Required(ErrorMessage = "{0}为必填选项")]
        [Range(0, 999999999, ErrorMessage = "{0}范围为{1}-{2}")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0}必须为正整数或0")]
        public int QuotaDay { get; set; }

        /// <summary>
        /// 排序序号
        /// </summary>
        [Display(Name = "排序序号")]
        [Required(ErrorMessage = "{0}为必填选项")]
        [Range(0, 999999999, ErrorMessage = "{0}范围为{1}-{2}")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0}必须为正整数或0")]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        [Required(ErrorMessage = "{0}为必填选项")]
        [StringLength(128, ErrorMessage = "{0}字符长度最大为{1}个字符")]
        public string Description { get; set; }
    }
}
