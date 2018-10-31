using System.ComponentModel.DataAnnotations;

namespace SexyColor.Web
{
    public class EditUserRanksModel
    {
        /// <summary>
        /// 级别  从1级开始
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 级别名称
        /// </summary>
        [Display(Name="级别名称")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public string RankName { get; set; }

        /// <summary>
        /// 经验值下限    （表示：某个等级最低要求多少经验）
        /// </summary>
        [Display(Name = "经验值下限")]
        [Required(ErrorMessage = "{0}为必填选项")]
        [Range(0,999999999,ErrorMessage ="{0}范围为{1}-{2}")]
        [RegularExpression(@"^\d+$",ErrorMessage ="{0}必须为正整数或0")]
        public int PointLower { get; set; }
    }
}