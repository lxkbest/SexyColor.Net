using SexyColor.CommonComponents;
using System;
using System.ComponentModel.DataAnnotations;

namespace SexyColor.Web
{
    public class DetailsUserProfilesModel
    {
        //12个属性

        /// <summary>
        /// 用户Id
        /// </summary>
        public long Userid { get; set; }

        /// <summary>
        /// 性别 0=男、1=女、-1:未填
        /// </summary>
        [Display(Name = "性别")]
        public int Sex { get; set; }

        /// <summary>
        /// 年龄 
        /// </summary>
        [Display(Name = "年龄")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "{0}为必填选项")]
        [IsPositiveIntegerType(ErrorMessage = "{0}只允许为正整数")]
        public int Age { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Display(Name = "生日")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0}为必填选项")]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 性取向 1:男性、2:女性、3:双性、-1:未填
        /// </summary>
        [Display(Name = "性取向")]
        public int SexualOrientation { get; set; }

        /// <summary>
        /// 性取向是否保密 0=否、1=是、-1:未填
        /// </summary>
        [Display(Name = "性取向是否保密")]
        public int IsSexualorientationSecrecy { get; set; }

        /// <summary>
        /// 婚姻状况 0=未婚、1=已婚、-1:未填
        /// </summary>
        [Display(Name = "婚姻状况")]
        public int Marriage { get; set; }

        /// <summary>
        /// 婚姻状况是否保密 0=否、1=是、-1:未填
        /// </summary>
        [Display(Name = "婚姻状况是否保密")]
        public int IsMarriageSecrecy { get; set; }

        /// <summary>
        /// 所属省
        /// </summary>
        [Display(Name = "所属省")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public string Provinces { get; set; }

        /// <summary>
        /// 所属市
        /// </summary>
        [Display(Name = "所属市")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public string City { get; set; }

        /// <summary>
        /// 所在地是否保密 0=否、1=是、-1:未填
        /// </summary>
        public int IsNowareaSecrecy { get; set; }

        /// <summary>
        /// 资料完整度
        /// </summary>
        [Display(Name = "资料完整度")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public int Integrity { get; set; }

    }
}
