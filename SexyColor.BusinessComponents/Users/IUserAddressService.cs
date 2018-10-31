using SexyColor.Infrastructure;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public interface IUserAddressService
    {
        /// <summary>
        /// 根据筛选条件 （获取分页组件）
        /// </summary>
        /// <param name="userQuery">筛选条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">显示条数</param>
        /// <returns></returns>
        PagingDataSet<UserAddress> GetUserAddress(UserAddressQuery userAddressQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <returns></returns>
        IEnumerable<BasicsProvinces> GetProvinces();

        /// <summary>
        /// 获取城市
        /// </summary>
        /// <returns></returns>
        IEnumerable<BasicsCitys> GetCitys(int pid);
        /// <summary>
        /// 获取区域
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        IEnumerable<BasicsAreas> GetAreas(int pid);


        void EnableUserAddress(IEnumerable<string> aids, bool isEnabled);

        UserAddress GetFullUserAddress(long userAddressId);

        /// <summary>
        /// 根据用户编号获取用户地址列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<UserAddress> GetUserAddressEntitysByUserid(long userId);

        /// <summary>
        /// 根据用户编号获取用户默认地址
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserAddress GetDefaultAddressByUserid(long userId);
    }
}
