using SexyColor.BusinessComponents;
using System;
using System.ComponentModel.DataAnnotations;

namespace SexyColor.Web
{
    public class CreatePermissionModel
    {
        /// <summary>
        /// 权限项
        /// </summary>
        [Display(Name = "权限项")]
        [Required(ErrorMessage = "权限Key不能为空")]
        public string Itemkey { get; set; }

        /// <summary>
        /// 应用编号
        /// </summary>
        [Display(Name = "应用编号")]
        [RegularExpression("^\\+?[1-9]\\d*$", ErrorMessage = "请选择一项应用编号")]
        public int Applicationid { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Display(Name = "权限名称")]
        [Required(ErrorMessage = "权限名称不能为空")]
        public string Itemname { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序号")]
        [Required(ErrorMessage = "排序不能为空")]
        [RegularExpression("^\\+?[0-9]\\d*$", ErrorMessage = "排序号只能是数字")]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 父级项
        /// </summary>
        [Display(Name = "父级项")]
        public string Parentsid { get; set; }

        /// <summary>
        /// 权限父级名称
        /// </summary>
        [Required(ErrorMessage = "权限父级不能为空")]
        public string ParentsName { get; set; }

        /// <summary>
        /// 权限Url
        /// </summary>
        [Display(Name = "权限Url")]
        [Required(ErrorMessage = "权限Url不能为空")]
        [RegularExpression("^(/\\w+){3}$", ErrorMessage = "权限Url格式不符合规则")]
        public string ItemUrl { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        [Display(Name = "权限类型")]
        [RegularExpression("^\\+?[1-9]\\d*$", ErrorMessage = "请选择一项权限类型")]
        public int ItemType { get; set; }

        /// <summary>
        /// 是否新功能
        /// </summary>
        [Display(Name = "是否新功能")]
        [RegularExpression("^\\+?[0-9]\\d*$", ErrorMessage = "请选择是否新功能")]
        public bool IsNewAction { get; set; }

        /// <summary>
        /// 将DetailsPermissionModel转换成PermissionItems
        /// </summary>
        /// <returns></returns>
        public PermissionItems AsPermissionItems()
        {
            PermissionItems permission = PermissionItems.New();
            permission.Applicationid = Applicationid;
            permission.DisplayOrder = DisplayOrder;
            permission.EventName = string.Empty;
            permission.IsEnable = true;
            permission.DateCreated = DateTime.UtcNow;
            permission.ItemUrl = ItemUrl;
            permission.Itemname = Itemname;
            permission.ItemType = ItemType;
            permission.Parentsid = Parentsid;
            return permission;
        }
    }
}
