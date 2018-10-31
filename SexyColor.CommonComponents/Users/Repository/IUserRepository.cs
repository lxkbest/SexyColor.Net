using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using System.Collections.Generic;

namespace SexyColor.CommonComponents
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user">待创建的用户</param>
        /// <param name="ignoreUsername">是否忽略禁用的用户名称</param>
        /// <param name="userCreateStatus">用户帐号创建状态</param>
        /// <returns>创建成功返回User，创建失败返回null</returns>
        User CreateUser(User user, bool ignoreUsername, out UserCreateStatus userCreateStatus);

        /// <summary>
        /// 注册验证
        /// </summary>
        /// <param name="user">待创建的用户</param>
        /// <param name="ignoreDisallowedUsername">是否忽略禁用的用户名称</param>
        /// <param name="userCreateStatus">用户帐号创建状态</param>
        void RegisterValidate(string userName, bool ignoreDisallowedUsername, out UserCreateStatus userCreateStatus);

        /// <summary>
        /// 更新实体（带缓存）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool UpdateCache(User entity);

        /// <summary>
        /// 删除实体（更新缓存）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool DeleteCache(User entity);
        /// <summary>
        /// 根据用户名获取用户Id
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户Id</returns>
        long GetUserIdByUserName(string userName);

        /// <summary>
        /// 根据昵称获取用户id
        /// </summary>
        /// <param name="nickName">用户昵称</param>
        /// <returns>用户id</returns>
        long GetUserIdByNickName(string nickName);

        /// <summary>
        /// 根据帐号邮箱获取用户
        /// </summary>
        /// <param name="accountEmail">帐号邮箱</param>
        /// <returns>用户Id</returns>
        long GetUserIdByEmail(string accountEmail);

        /// <summary>
        /// 根据手机号获取用户
        /// </summary>
        /// <param name="accountMobile">手机号</param>
        /// <returns>用户Id</returns>
        long GetUserIdByMobile(string accountMobile);

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回User 或者 null</returns>
        User GetUser(long userId);

        /// <summary>
        /// 获取用户集合（分页）
        /// </summary>
        /// <param name="userQuery">查询条件</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">显示条数</param>
        /// <returns></returns>
        PagingDataSet<User> GetUsers(UserQuery userQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 根据用户id集合获取用户集合（分页）
        /// </summary>
        /// <param name="ids">用户id集合</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">显示条数</param>
        /// <returns></returns>
        PagingDataSet<User> GetUsers(IEnumerable<long> ids, int pageIndex, int pageSize);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        bool ResetPassword(User user, string newPassword);

        /// <summary>
        /// 设置头像
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="headImage">头像url路径</param>
        /// <returns></returns>
        bool SetHeadIimage(User user, string headImage);
    }
}
