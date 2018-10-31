namespace SexyColor.Web
{
    public class DetailsUserAddressModel
    {

        /// <summary>
        /// 收件人
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 收件地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 是否默认地址  0: 否，1:是
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 是否启用  0: 否，1:是
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 省市区
        /// </summary>
        public string ProvinceCityArea { get; set; }

    }
}
