using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;

namespace SexyColor.CommonComponents
{
    public class MembershipService : IMembershipService
    {
        public IUserRepository userRepository { get; set; }

        /// <summary>
        /// 验证用户名和密码是否匹配
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns>登录状态枚举</returns>
        public UserLoginStatus ValidateUser(string userName, string passWord)
        {
            long userId = UserIdToUserNameDictionary.GetUserId(userName);
            User user = userRepository.GetUser(userId);
            if (user == null)
                return UserLoginStatus.InvalidCredentials;
            if(!userName.Equals(user.UserName, StringComparison.CurrentCulture))
                return UserLoginStatus.InvalidCredentials;
            if (!UserPasswordHelper.CheckPassword(passWord, user.Password, (UserPasswordFormat)user.PasswordFormat))
                return UserLoginStatus.InvalidCredentials;
            if (!user.IsActivated)
                return UserLoginStatus.NotActivated;
            if (user.IsBanned)
            {
                if (user.BanDeadLine >= DateTime.UtcNow)
                    return UserLoginStatus.Banned;
                else
                {
                    user.IsBanned = false;
                    user.BanDeadLine = DateTime.UtcNow;
                    userRepository.Update(user);
                }
            }

            return UserLoginStatus.Success;
        }

        /// <summary>
        /// 批量处理激活，取消激活操作
        /// </summary>
        /// <param name="userIds">用户编号集合</param>
        /// <param name="isActivated">是否激活</param>
        /// <returns></returns>
        public void ActivatedUsers(IEnumerable<long> userIds, bool isActivated)
        {
            foreach (var userId in userIds)
            {
                User user = userRepository.GetUser(userId);
                if (user == null)
                    continue;

                if (user.IsActivated == isActivated)
                    continue;

                user.IsActivated = isActivated;
                user.ForceLogin = !isActivated;
                userRepository.UpdateCache(user);
            }
        }

        /// <summary>
        /// 批量取消封禁操作
        /// </summary>
        /// <param name="userIds">用户编号集合</param>
        public void UnBannedUsers(IEnumerable<long> userIds)
        {
            foreach (var userId in userIds)
            {
                User user = userRepository.GetUser(userId);
                if (user == null)
                    continue;

                user.IsBanned = false;
                user.BanDeadLine = DateTime.UtcNow;
                user.BanReason = string.Empty;
                user.ForceLogin = false;
                userRepository.UpdateCache(user);
            }
        }

        /// <summary>
        /// 批量封禁操作
        /// </summary>
        /// <param name="userIds">用户编号集合</param>
        /// <param name="banDeadline">截止时间</param>
        /// <param name="banReason">封禁原因</param>
        public void BannedUsers(IEnumerable<long> userIds, DateTime banDeadline, string banReason)
        {
            List<User> users = new List<User>();
            foreach (var userId in userIds)
            {
                User user = userRepository.GetUser(userId);
                if (user == null)
                    continue;

                user.IsBanned = true;
                user.ForceLogin = true;
                user.BanDeadLine = banDeadline;
                user.BanReason = banReason;

                userRepository.UpdateCache(user);
            }
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="userCreateStatus"></param>
        /// <returns></returns>
        public User CreateUser(User user, string password, out UserCreateStatus userCreateStatus)
        {
            return CreateUser(user, password, string.Empty, string.Empty, false, out userCreateStatus);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password">密码</param>
        /// <param name="passwordQuestion">密码问题</param>
        /// <param name="passwordAnswer">密码答案</param>
        /// <param name="ignoreUsername">是否忽略禁用的用户名称</param>
        /// <param name="userCreateStatus">创建状态</param>
        /// <returns></returns>
        public User CreateUser(User user, string password, string passwordQuestion, string passwordAnswer, bool ignoreUsername, out UserCreateStatus userCreateStatus)
        {
            if (user == null)
            {
                userCreateStatus = UserCreateStatus.UnknownFailure;
                return null;
            }
            string errorMessage = string.Empty;
            if (!Utility.ValidatePassword(password, out errorMessage))
            {
                userCreateStatus = UserCreateStatus.InvalidPassword;
                return null;
            }
            user.PasswordFormat = (int)UserPasswordFormat.MD5;
            user.Password = UserPasswordHelper.EncodePassword(password, UserPasswordFormat.MD5);
            user.PasswordQuestion = passwordQuestion;
            user.PasswordAnswer = passwordAnswer;
            user.IsModerated = false;

            user = userRepository.CreateUser(user, ignoreUsername, out userCreateStatus);
            return user;
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="password">需要加密的密码</param>
        /// <returns></returns>
        public bool ResetPassword(User user, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                return false;
            
            var md5Password = UserPasswordHelper.EncodePassword(newPassword, UserPasswordFormat.MD5);
            var result = userRepository.ResetPassword(user, md5Password);
            return result;
        }

        /// <summary>
        /// 设置头像
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="headImage">图片Url地址</param>
        /// <returns></returns>
        public User SetHeadImage(User user, string headImage)
        {
            if (string.IsNullOrWhiteSpace(headImage))
                return null;

            var result = userRepository.SetHeadIimage(user, headImage);
            if (result)
                user.HeadImg = headImage;
            return user;
        }
    }
}
