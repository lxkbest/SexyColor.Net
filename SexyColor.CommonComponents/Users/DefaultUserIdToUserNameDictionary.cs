using SexyColor.BusinessComponents;

namespace SexyColor.CommonComponents
{
    public class DefaultUserIdToUserNameDictionary : UserIdToUserNameDictionary
    {
        public IUserRepository userRepository { get; set; }

        /// <summary>
        /// 根据用户Id获取用户名
        /// </summary>
        /// <returns>
        /// 用户名
        /// </returns>
        protected override string GetUserNameByUserId(long userId)
        {
            User user = userRepository.GetUser(userId);
            if (user != null)
                return user.UserName;
            return null;
        }

        /// <summary>
        /// 根据用户名获取用户Id
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>
        /// 用户Id
        /// </returns>
        protected override long GetUserIdByUserName(string userName)
        {
            return userRepository.GetUserIdByUserName(userName);
        }
    }
}
