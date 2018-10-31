using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.CommonComponents
{
    public static class UserServiceExtension
    {
        /// <summary>
        /// 根据用户名查询器获取用户实体
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static User GetFullUser(this IUserService userService, string userName)
        {
            long userId = UserIdToUserNameDictionary.GetUserId(userName);
            return userService.GetUserRepository().GetUser(userId);
        }

        /// <summary>
        /// 根据用户Id查询器获取用户实体
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId">用户ID</param>        
        public static User GetFullUser(this IUserService userService, long userId)
        {
            string userName = UserIdToUserNameDictionary.GetUserName(userId);
            return userService.GetUserRepository().GetUser(userId);
        }

        /// <summary>
        /// 根据筛选条件 （获取分页组件）
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userQuery">筛选条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">显示条数</param>
        /// <returns></returns>
        public static PagingDataSet<User> GetUsers(this IUserService userService, UserQuery userQuery, int pageIndex, int pageSize)
        {
            IUserRepository repository = userService.GetUserRepository();
            return repository.GetUsers(userQuery, pageIndex, pageSize);

        }

        /// <summary>
        /// 根据用户id集合（获取分页组件）
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="ids">用户id集合</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">显示条数</param>
        /// <returns></returns>
        public static PagingDataSet<User> GetUsers(this IUserService userService, IEnumerable<long> ids, int pageIndex, int pageSize)
        {
            IUserRepository repository = userService.GetUserRepository();
            return repository.GetUsers(ids, pageIndex, pageSize);

        }

        /// <summary>
        /// 获取用户数据访问实例
        /// </summary>
        /// <param name="userService"></param>
        /// <returns></returns>
        private static IUserRepository GetUserRepository(this IUserService userService)
        {
            IUserRepository userRepository = DIContainer.Resolve<IUserRepository>();
            if (userRepository == null)
                userRepository = new UserRepository();
            return userRepository;
        }

        
    }
}
