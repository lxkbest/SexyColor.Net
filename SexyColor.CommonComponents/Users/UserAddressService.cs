using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using System.Collections.Generic;

namespace SexyColor.CommonComponents
{
    public class UserAddressService : IUserAddressService
    {
        public IUserAddressRepository userAddressRepository { get; set; }
        public IBasicsProvincesRepository basicsProvincesRepository { get; set; }
        public IBasicsCitysRepository basicsCitysRepository { get; set; }
        public IBasicsAreasRepository basicsAreasRepository { get; set; }

        /// <summary>
        /// 根据筛选条件 （获取分页组件）
        /// </summary>
        /// <param name="userQuery">筛选条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">显示条数</param>
        /// <returns></returns>
        public PagingDataSet<UserAddress> GetUserAddress(UserAddressQuery userAddressQuery, int pageIndex, int pageSize)
        {
            return userAddressRepository.GetUserAddress(userAddressQuery, pageIndex, pageSize);
        }
        /// <summary>
        /// 获取省份
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BasicsProvinces> GetProvinces()
        {
            return basicsProvincesRepository.GetProvinces();
        }
        /// <summary>
        /// 获取城市
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BasicsCitys> GetCitys(int pid)
        {
            return basicsCitysRepository.GetCitys(pid);
        }
        /// <summary>
        /// 获取区域
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public IEnumerable<BasicsAreas> GetAreas(int pid)
        {
            return basicsAreasRepository.GetAreas(pid);
        }
        /// <summary>
        /// 获取用户地址实体
        /// </summary>
        /// <returns></returns>
        public UserAddress GetFullUserAddress(long userAddressId)
        {
            return userAddressRepository.GetUserAddress(userAddressId);
        }

        public void EnableUserAddress(IEnumerable<string> aids, bool isEnabled)
        {
            foreach (string aId in aids)
            {
                UserAddress userAddress = userAddressRepository.GetUserAddress(long.Parse(aId));
                if (userAddress == null) continue;

                if (userAddress.IsDeleted == isEnabled) continue;

                userAddress.IsDeleted = isEnabled;

                userAddressRepository.UpdateCache(userAddress);
            }
        }

        /// <summary>
        /// 根据用户编号获取用户地址列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<UserAddress> GetUserAddressEntitysByUserid(long userId)
        {
            return userAddressRepository.GetUserAddressEntitysByUserid(userId);
        }

        /// <summary>
        /// 根据用户编号获取用户默认地址
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserAddress GetDefaultAddressByUserid(long userId)
        {
            return userAddressRepository.GetDefaultAddressByUserid(userId);
        }
    }
}
