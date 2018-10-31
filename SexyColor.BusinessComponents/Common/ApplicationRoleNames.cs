using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// 应用管理员处理器
    /// </summary>
    public class ApplicationRoleNames
    {
        private static ConcurrentDictionary<int, IEnumerable<string>> applicationAdministratorRoleNames = new ConcurrentDictionary<int, IEnumerable<string>>();

        /// <summary>
        /// 获取应用管理员角色
        /// </summary>
        /// <param name="applicationId">应用Id</param>
        /// <returns>应用管理员角色</returns>
        public static IEnumerable<string> GetAll()
        {
            List<string> roleNames = new List<string>();
            foreach (var item in applicationAdministratorRoleNames.Values)
            {
                roleNames.AddRange(item);
            }
            return roleNames;
        }

        /// <summary>
        /// 获取应用管理员角色
        /// </summary>
        /// <param name="applicationId">应用Id</param>
        /// <returns>应用管理员角色</returns>
        public static IEnumerable<string> GetRoleNames(int applicationId)
        {
            IEnumerable<string> roleNames;
            if (applicationAdministratorRoleNames.TryGetValue(applicationId, out roleNames))
                return roleNames;

            return null;
        }

        /// <summary>
        /// 添加应用所属的角色
        /// </summary>
        /// <param name="applicationId">应用Id</param>
        /// <param name="applicationAdministratorRoleNames">应用管理员角色</param>
        public static void Add(int applicationId, IEnumerable<string> roleNames)
        {
            if (applicationId <= 0 || roleNames == null)
                return;
            applicationAdministratorRoleNames[applicationId] = roleNames;
        }

        /// <summary>
        /// 删除应用所属的角色
        /// </summary>
        /// <param name="applicationId">应用Id</param>
        public static void Remove(int applicationId)
        {
            applicationAdministratorRoleNames.TryRemove(applicationId, out var roleNames);
        }
    }
}
