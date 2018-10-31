using System.Collections.Generic;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IUserService
    {
        /// <summary>
        /// 根据用户昵称获取用户
        /// </summary>
        /// <param name="nickName"></param>
        /// <returns></returns>
        User GetUserByNickName(string nickName);

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        User GetUser(long userId);

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName">用户名</param>
        User GetUser(string userName);

        /// <summary>
        /// 根据账号邮箱获取用户
        /// </summary>
        /// <param name="accountEmail">账号邮箱</param>
        User FindUserByEmail(string accountEmail);

        /// <summary>
        /// 根据手机号获取用户
        /// </summary>
        /// <param name="accountMobile">手机号</param>
        User FindUserByMobile(string accountMobile);

        /// <summary>
        /// 根据UserId集合组装IUser集合
        /// </summary>
        /// <param name="userIds">用户Id集合</param>
        /// <returns></returns>
        IEnumerable<User> GetUsers(IEnumerable<long> userIds);

        /// <summary>
        /// 根据UserId集合删除用户
        /// </summary>
        /// <param name="userIds"></param>
        void DeleteUsers(IEnumerable<long> userIds);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        void DeleteUser(long userId);

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool EditUser(User entity);

    }
}
