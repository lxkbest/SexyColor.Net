using System.ComponentModel.DataAnnotations;

namespace SexyColor.Web
{
    public class EditPointItemsModel
    {
        /// <summary>
        /// 积分项目标识
        /// </summary>
        public int Itemskey { get; set; }

        /// <summary>
        /// 积分项目名称
        /// </summary>
        [Display(Name = "积分项目名称")]
        [Required(ErrorMessage = "{0}为必填选项")]
        [StringLength(32, ErrorMessage = "{0}字符长度最大为{1}个字符")]
        public string Itemsname { get; set; }

        /// <summary>
        /// 积分类型标识   
        /// </summary>
        [Display(Name = "积分类型标识")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public string Itemstyle { get; set; }

        /// <summary>
        /// 模块Id
        /// </summary>
        [Display(Name = "模块ID")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public int Applicationid { get; set; }

        /// <summary>
        /// 积分值
        /// </summary>
        [Display(Name = "积分值")]
        [Required(ErrorMessage = "{0}为必填选项")]
        [Range(0, 999999999, ErrorMessage = "{0}范围为{1}-{2}")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0}必须为正整数或0")]
        public int SexyPoints { get; set; }

        /// <summary>
        /// 经验值
        /// </summary>
        [Display(Name = "经验值")]
        [Required(ErrorMessage = "{0}为必填选项")]
        [Range(0, 999999999, ErrorMessage = "{0}范围为{1}-{2}")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0}必须为正整数或0")]
        public int ExperiencePoints { get; set; }

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
