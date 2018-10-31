using System.ComponentModel.DataAnnotations;

namespace SexyColor.BusinessComponents
{
    public enum PermissionItemsType
    {
        /// <summary>
        /// 左侧菜单
        /// </summary>
        [Display(Name = "左侧菜单")]
        LeftMenu = 1,

        /// <summary>
        /// 普通按钮
        /// </summary>
        [Display(Name = "普通按钮")]
        OrdinaryButton = 2
    }

    public enum PermissionItemsCreateStatus
    {
        /// <summary>
        /// 未知错误
        /// </summary>
        UnknownFailure = 0,

        /// <summary>
        /// 创建成功
        /// </summary>
        Created = 1,

        /// <summary>
        /// 父级项要么是Guid要么是0
        /// </summary>
        GuidFailure = 2,

        /// <summary>
        /// 权限Url不正确
        /// </summary>
        UrlFailure = 3,

        /// <summary>
        /// 更新成功
        /// </summary>
        Updated = 4,
    }
}
