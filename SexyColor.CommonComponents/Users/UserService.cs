using System.Collections.Generic;
using SexyColor.BusinessComponents;

namespace SexyColor.CommonComponents
{
    public class UserService : IUserService
    {
        public IUserRepository userRepository { get; set; }

        /// <summary>
        /// 根据帐号邮箱获取用户
        /// </summary>
        /// <param name="accountEmail">帐号邮箱</param>
        public User FindUserByEmail(string accountEmail)
        {
            long userId = userRepository.GetUserIdByEmail(accountEmail);
            return GetUser(userId);
        }
        /// <summary>
        /// 根据手机号获取用户
        /// </summary>
        /// <param name="accountMobile">手机号</param>
        public User FindUserByMobile(string accountMobile)
        {
            long userId = userRepository.GetUserIdByMobile(accountMobile);
            return GetUser(userId);
        }
        /// <summary>
        /// 获取用户根据用户Id
        /// </summary>
        /// <param name="userId">用户Id</param>
        public User GetUser(long userId)
        {
            if (userId <= 0)
                return null;
            return userRepository.GetUser(userId);
        }
        /// <summary>
        /// 获取用户根据用户名
        /// </summary>
        /// <param name="userName">用户名</param>
        public User GetUser(string userName)
        {
            long userId = userRepository.GetUserIdByUserName(userName);
            return GetUser(userId);
        }

        /// <summary>
        /// 根据昵称获取昵称
        /// </summary>
        /// <param name="nickName">昵称</param>
        public User GetUserByNickName(string nickName)
        {
            long userId = userRepository.GetUserIdByNickName(nickName);
            return GetUser(userId);
        }

        /// <summary>
        /// 根据用户Id集合获取用户对象集合
        /// </summary>
        /// <param name="userIds">用户Id集合</param>
        /// <returns>用户对象集合</returns>
        public IEnumerable<User> GetUsers(IEnumerable<long> userIds)
        {
            if (userIds == null)
                return new List<User>();
            return userRepository.GetModelByIds<long>(w => w.UserId, userIds);
        }

        /// <summary>
        /// 根用户Id集合删除用户
        /// </summary>
        /// <param name="userIds">用户Id集合</param>
        public void DeleteUsers(IEnumerable<long> userIds)
        {
            List<User> users = new List<User>();
            foreach (var userId in userIds)
            {
                User user = userRepository.GetUser(userId);
                if (user == null)
                    continue;
                var result = userRepository.DeleteCache(user);

                if (result) {
                    UserIdToUserNameDictionary.RemoveUserId(userId);
                    UserIdToUserNameDictionary.RemoveUserName(user.UserName);
                }
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户Id</param>
        public void DeleteUser(long userId)
        {
            User user = userRepository.GetUser(userId);
            var result = userRepository.DeleteCache(user);
            if (result)
            {
                UserIdToUserNameDictionary.RemoveUserId(userId);
                UserIdToUserNameDictionary.RemoveUserName(user.UserName);
            }
        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool EditUser(User entity)
        {
            return userRepository.UpdateCache(entity);
        }
    }
}
