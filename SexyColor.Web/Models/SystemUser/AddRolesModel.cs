using Microsoft.AspNetCore.Mvc;
using SexyColor.CommonComponents;
using System;
using System.ComponentModel.DataAnnotations;
using SexyColor.CommonComponents.Mvc.Validation;

namespace SexyColor.Web
{
    public class AddRolesModel
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Display(Name = "角色名称")]
        [Remote("ValidateRolesName", "System")]
        public string Rolename { get; set; }

        /// <summary>
        /// 角色友好名称用于对外显示  (可用可不用)
        /// </summary>
        [Display(Name = "角色友好名称")]
        [StringLength(20, ErrorMessage = "{0}最大长度为{1}个字符")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public string FriendlyRolename { get; set; }

        /// <summary>
        /// 是否系统内置默认  0：否、1：是
        /// </summary>
        [Display(Name = "是否系统内置默认")]
        public bool IsBuiltin { get; set; }

        /// <summary>
        /// 是否直接关联到用户  0：否、1：是
        /// </summary>
        [Display(Name = "是否直接关联到用户")]
        public bool ConnectToUser { get; set; }

        /// <summary>
        /// 是否对外显示 0：否、1：是
        /// </summary>
        [Display(Name = "是否对外显示")]
        public bool IsPublic { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        [Required(ErrorMessage = "{0}为必填选项")]
        [StringLength(50, ErrorMessage = "{0}最大长度为{1}个字符")]
        public string Description { get; set; }

        /// <summary>
        /// 是否启用 0：否、1：是
        /// </summary>
        [Display(Name = "是否启用")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 角色标识图片
        /// </summary>
        [Display(Name = "角色标识图片")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public string RoleImage { get; set; }

        /// <summary>
        /// 所属应用
        /// </summary>
        [Display(Name = "所属应用")]
        public int Applicationid { get; set; }
    }
}
