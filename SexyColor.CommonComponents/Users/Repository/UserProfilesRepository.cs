using System;
using SexyColor.Infrastructure;
using SexyColor.BusinessComponents;

namespace SexyColor.CommonComponents
{
    public class UserProfilesRepository : Repository<UserProfiles>, IUserProfilesRepository
    {
        

        /// <summary>
        /// 添加实体 （带缓存）
        /// </summary>
        /// <param name="entity">实体</param>
        public bool AddCache(UserProfiles entity)
        {
            object result = base.AddByCache(entity, false);
            bool value = false;
            bool.TryParse(result.ToString(), out value);
            return value;
        }
        /// <summary>
        /// 获取用户资料实体
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserProfiles GetUserProfiles(long userId)
        {
            return base.GetByCache(m=>m.Userid==userId,userId);
        }

        /// <summary>
        /// 更新实体（带缓存）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public bool UpdateCache(UserProfiles entity)
        {
            return base.UpdateByCache(entity);
        }

        /// <summary>
        /// 更新完成度
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="integrity">完成度</param>
        public bool UpdateIntegrity(long userId, int integrity)
        {
            UserProfiles entity = base.GetByCache(w => w.Userid == userId, userId);
            entity.Integrity = integrity;
            return base.UpdateByCache(entity, new { integrity = integrity }, w => w.Userid == entity.Userid);
        }

        /// <summary>
        /// 判断是否存在用户资料
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public bool UserIdIsExist(long userId)
        {
            return base.Exist(w => w.Userid == userId);
        }

    }
}
