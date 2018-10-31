using System;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// 用户账户业务逻辑接口
    /// </summary>
    public interface IMembershipService
    {
        /// <summary>
        /// 验证用户（登录使用）
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        UserLoginStatus ValidateUser(string userName, string passWord);

        /// <summary>
        /// 批量处理激活，取消激活操作
        /// </summary>
        /// <param name="userIds">用户编号集合</param>
        /// <param name="isActivated">是否激活</param>
        /// <returns></returns>
        void ActivatedUsers(IEnumerable<long> userIds, bool isActivated);

        /// <summary>
        /// 批量取消封禁操作
        /// </summary>
        /// <param name="userIds">用户编号集合</param>
        void UnBannedUsers(IEnumerable<long> userIds);

        /// <summary>
        /// 批量封禁操作
        /// </summary>
        /// <param name="userIds">用户编号集合</param>
        /// <param name="banDeadline">截止时间</param>
        /// <param name="banReason">封禁原因</param>
        void BannedUsers(IEnumerable<long> userIds, DateTime banDeadline, string banReason);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user">转换后的数据库用户实体</param>
        /// <param name="password">密码</param>
        /// <param name="userCreateStatus">状态</param>
        /// <returns></returns>
        User CreateUser(User user, string password, out UserCreateStatus userCreateStatus);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user">转换后的数据库用户实体</param>
        /// <param name="password">密码</param>
        /// <param name="passwordQuestion">密码问题</param>
        /// <param name="passwordAnswer">密码答案</param>
        /// <param name="ignoreUsername">是否忽略禁用的用户名称</param>
        /// <param name="userCreateStatus">状态</param>
        /// <returns></returns>
        User CreateUser(User user, string password, string passwordQuestion, string passwordAnswer, bool ignoreUsername, out UserCreateStatus userCreateStatus);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="password">需要重置的密码</param>
        /// <returns></returns>
        bool ResetPassword(User user, string password);

        /// <summary>
        /// 设置头像
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="headImage">图片Url地址</param>
        /// <returns></returns>
        User SetHeadImage(User user, string headImage);
    }

}
