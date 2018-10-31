namespace SexyColor.BusinessComponents
{
    public class RolesQuery
    {
        /// <summary>
        ///  名称（角色名、对外显示名称）
        /// </summary>
        public string Keyword = string.Empty;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnabled = null;

        /// <summary>
        /// 是否是内置用户
        /// </summary>
        public bool? IsBuiltin = null;

        /// <summary>
        /// 是否公开
        /// </summary>
        public bool? IsPublic = null;

        /// <summary>
        /// 排序方式
        /// </summary>
        public RoleSortBy? RoleSortBy = null;
    }

    /// <summary>
    /// 排序方式
    /// </summary>
    public enum RoleSortBy
    {
        /// <summary>
        /// 根据rolename排序
        /// </summary>
        Rolename = 1,

        /// <summary>
        /// 根据rolename排序倒序排列
        /// </summary>
        Rolename_Desc = 2,

        /// <summary>
        /// 根据是否启用排序
        /// </summary>
        IsEnabled = 3,
    }
}
