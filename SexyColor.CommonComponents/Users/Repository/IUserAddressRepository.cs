using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using System.Collections.Generic;

namespace SexyColor.CommonComponents
{
    public interface IUserAddressRepository : IRepository<UserAddress>
    {
        PagingDataSet<UserAddress> GetUserAddress(UserAddressQuery userAddressQuery, int pageIndex, int pageSize);
        UserAddress GetUserAddress(long userId);
        bool UpdateCache(UserAddress userAddress);

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
