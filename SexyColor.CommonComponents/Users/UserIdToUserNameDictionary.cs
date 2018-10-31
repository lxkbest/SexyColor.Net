using System;
using System.Collections.Concurrent;
using SexyColor.Infrastructure;

namespace SexyColor.CommonComponents
{

    public abstract class UserIdToUserNameDictionary
    {
        // 线程安全的字典 id取用户名
        private static ConcurrentDictionary<long, string> dictionaryOfUserIdToUserName = new ConcurrentDictionary<long, string>();
        // 线程安全的字典 用户名取id
        private static ConcurrentDictionary<string, long> dictionaryOfUserNameToUserId = new ConcurrentDictionary<string, long>();
        // 自身
        private static volatile UserIdToUserNameDictionary _defaultInstance = null;
        // 锁
        private static readonly object lockObject = new object();

        /// <summary>
        /// 获取自身实例
        /// </summary>
        /// <returns></returns>
        private static UserIdToUserNameDictionary Instance()
        {
            if (_defaultInstance == null)
            {
                lock (lockObject)
                {
                    if (_defaultInstance == null)
                    {
                        _defaultInstance = DIContainer.Resolve<UserIdToUserNameDictionary>();
                        if (_defaultInstance == null)
                            throw new Exception("未在DIContainer注册UserIdToUserNameDictionary的具体实现类");
                    }
                }
            }
            return _defaultInstance;
        }

        /// <summary>
        /// 根据用户名获取用户id
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        protected abstract long GetUserIdByUserName(string userName);
        /// <summary>
        /// 根据用户id获取用户名
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        protected abstract string GetUserNameByUserId(long userId);

        /// <summary>
        /// 根据查询器快速获取用户Id
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static long GetUserId(string userName)
        {
            if (dictionaryOfUserNameToUserId.ContainsKey(userName))
                return dictionaryOfUserNameToUserId[userName];
            long userId = Instance().GetUserIdByUserName(userName);
            if (userId > 0)
            {
                dictionaryOfUserNameToUserId[userName] = userId;
                if (!dictionaryOfUserIdToUserName.ContainsKey(userId))
                    dictionaryOfUserIdToUserName[userId] = userName;
            }
            return userId;
        }

        /// <summary>
        /// 根据查询器快速获取用户名
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public static string GetUserName(long userId)
        {
            if (dictionaryOfUserIdToUserName.ContainsKey(userId))
                return dictionaryOfUserIdToUserName[userId];
            string userName = Instance().GetUserNameByUserId(userId);
            if (!string.IsNullOrEmpty(userName))
            {
                dictionaryOfUserIdToUserName[userId] = userName;
                if (!dictionaryOfUserNameToUserId.ContainsKey(userName))
                    dictionaryOfUserNameToUserId[userName] = userId;
            }
            return userName;
        }

        /// <summary>
        /// 移除UserId
        /// </summary>
        /// <param name="userId">userId</param>
        internal static void RemoveUserId(long userId)
        {
            string userName;
            dictionaryOfUserIdToUserName.TryRemove(userId, out userName);
        }

        /// <summary>
        /// 移除UserId
        /// </summary>
        /// <param name="userName">userName</param>
        internal static void RemoveUserName(string userName)
        {
            long userId;
            dictionaryOfUserNameToUserId.TryRemove(userName, out userId);
        }

    }
}
