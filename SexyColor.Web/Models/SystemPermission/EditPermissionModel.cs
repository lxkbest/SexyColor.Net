using SexyColor.BusinessComponents;
using System;
using System.ComponentModel.DataAnnotations;

namespace SexyColor.Web
{
    public class EditPermissionModel
    {
        /// <summary>
        /// 权限项
        /// </summary>
        [Display(Name = "权限项")]
        [Required(ErrorMessage = "权限Key不能为空")]
        public string Itemkey { get; set; }

        /// <summary>
        /// 应用Id 
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
        /// 权限父级
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
        [RegularExpression("^(/\\w+){3}$|^/$", ErrorMessage = "权限Url格式不符合规则")]
        public string ItemUrl { get; set; }

        /// <summary>
        /// 事件或者按钮名
        /// </summary>
        [Display(Name = "事件或者按钮名")]
        public string EventName { get; set; }

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
        public int IsNewAction { get; set; }


        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        [RegularExpression("^\\+?[0-9]\\d*$", ErrorMessage = "请选择是否启用")]
        public int IsEnable { get; set; }

        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 将PermissionItems转换DetailsPermissionModel 采用链式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public EditPermissionModel ToDetailsPermissionModel(PermissionItems item)
        {
            Itemkey = item.Itemkey;
            Applicationid = item.Applicationid;
            Itemname = item.Itemname;
            Parentsid = item.Parentsid;
            IsEnable = item.IsEnable ? 1 : 0;
            DisplayOrder = item.DisplayOrder;
            ItemType = item.ItemType;
            IsNewAction = item.IsNewAction ? 1 : 0;
            EventName = item.EventName;
            ItemUrl = item.ItemUrl;
            DateCreated = item.DateCreated;
            return this;
        }

        public PermissionItems AsPermissionItems()
        {
            PermissionItems item = new PermissionItems();
            item.Itemkey = Itemkey;
            item.Itemname = Itemname;
            item.Parentsid = Parentsid;

            item.ItemType = ItemType;
            item.ItemUrl = ItemUrl;
            item.Applicationid = Applicationid;
            item.DateCreated = DateCreated;

            item.DisplayOrder = DisplayOrder;
            item.EventName = EventName ?? string.Empty;
            item.IsEnable = IsEnable <= 0 ? false : true;
            item.IsNewAction = IsNewAction <= 0 ? false : true;
            return item;
        }
    }
}
