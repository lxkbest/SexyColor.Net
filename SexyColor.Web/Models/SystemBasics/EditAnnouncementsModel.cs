using System.ComponentModel.DataAnnotations;

namespace SexyColor.Web
{
    public class EditAnnouncementsModel
    {
        /// <summary>
        /// 站点公告标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 公告主题
        /// </summary>
        [Display(Name = "公告主题")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public string Subject { get; set; }

        /// <summary>
        /// 公告内容
        /// </summary>
        [Display(Name = "公告内容")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public string Body { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        [Display(Name = "发布时间")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public System.DateTime DateRelease { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [Display(Name = "过期时间")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public System.DateTime DateExpired { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public System.DateTime DateLastupdate { get; set; }

        /// <summary>
        /// 是否启用   0=否、1=是
        /// </summary>
        [Display(Name = "是否启用")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Display(Name = "显示顺序")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public long Userid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime DateCreate { get; set; }
    }
}
