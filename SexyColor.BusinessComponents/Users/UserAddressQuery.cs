namespace SexyColor.BusinessComponents
{
    public class UserAddressQuery
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long? UserId = null;

        /// <summary>
        /// 收件人
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName = string.Empty;

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone = string.Empty;

        /// <summary>
        /// 收件地址
        /// </summary>
        public string Address = string.Empty;

        /// <summary>
        /// 是否默认地址  0: 否，1:是
        /// </summary>
        public bool? IsDefault = null;

        /// <summary>
        /// 是否启用  0: 否，1:是
        /// </summary>
        public bool? IsDeleted = null;

        /// <summary>
        /// 省市区
        /// </summary>
        public string ProvinceCityArea = string.Empty;

        /// <summary>
        /// 省份编号
        /// </summary>
        public int? Provinceid = null;

        /// <summary>
        /// 城市编号
        /// </summary>
        public int? Cityid = null;

        /// <summary>
        /// 城区编号
        /// </summary>
        public int? Areaid = null;

        /// <summary>
        /// 排序方式
        /// </summary>
        public UserAddressSortBy? UserAddressSortBy = null;
    }

    /// <summary>
    /// 排序方式
    /// </summary>
    public enum UserAddressSortBy
    {
        /// <summary>
        /// 根据收件人排序
        /// </summary>
        Name = 1,

        /// <summary>
        /// 根据收件人倒序排列
        /// </summary>
        Name_Desc = 2,

        /// <summary>
        /// 根据手机号码排序
        /// </summary>
        Phone = 3,

        /// <summary>
        /// 根据手机号码倒序
        /// </summary>
        Phone_Desc = 4
    }
}
