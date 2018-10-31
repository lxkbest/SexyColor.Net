using SexyColor.Infrastructure;

namespace SexyColor.CommonComponents
{
    public interface IUserProfilesRepository : IRepository<UserProfiles>
    {

        /// <summary>
        /// 判断是否存在用户资料
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool UserIdIsExist(long userId);

        /// <summary>
        /// 更新完成度
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="integrity">完成度</param>
        bool UpdateIntegrity(long userId, int integrity);

        /// <summary>
        /// 更新实体（带缓存）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool UpdateCache(UserProfiles entity);

        /// <summary>
        /// 添加实体 （带缓存）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool AddCache(UserProfiles entity);
        /// <summary>
        /// 获取用户资料实体
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserProfiles GetUserProfiles(long userId);
    }
}
