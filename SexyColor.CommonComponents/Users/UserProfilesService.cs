using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using System;

namespace SexyColor.CommonComponents
{
    public class UserProfilesService
    {
        public IUserProfilesRepository userProfilesRepository { get; set; }
        public IUserRepository userRepository { get; set; }

        /// <summary>
        /// 创建用户资料
        /// </summary>
        /// <param name="userProfiles"></param>
        /// <returns></returns>
        public bool Create(UserProfiles userProfiles)
        {
            if (userProfiles == null)
                return false;

            if (userProfilesRepository.UserIdIsExist(userProfiles.Userid))
            {
                userProfilesRepository.UpdateCache(userProfiles);
                return false;
            }

            bool result = userProfilesRepository.AddCache(userProfiles);
            User user = userRepository.GetUser(userProfiles.Userid);
            UserProfiles newUserProfiles = userProfilesRepository.GetByCache(w => w.Userid == userProfiles.Userid, userProfiles.Userid);
            UserProfilesSettings settings = new UserProfilesSettings();


            int integrity = newUserProfiles.Birthday != DateTime.MinValue ? settings.IntegrityProportions[(int)ProfileIntegrityItems.Birthday] : 0;
            integrity += user.IsHeadImg ? settings.IntegrityProportions[(int)ProfileIntegrityItems.Headimg] : 0;
            integrity += newUserProfiles.IsSex ? settings.IntegrityProportions[(int)ProfileIntegrityItems.Sex] : 0;
            integrity += newUserProfiles.IsAge ? settings.IntegrityProportions[(int)ProfileIntegrityItems.Age] : 0;
            integrity += newUserProfiles.IsSexualOrientation ? settings.IntegrityProportions[(int)ProfileIntegrityItems.SexualOrientation] : 0;
            integrity += newUserProfiles.IsMarriage ? settings.IntegrityProportions[(int)ProfileIntegrityItems.Sarriage] : 0;
            integrity += newUserProfiles.IsArea ? settings.IntegrityProportions[(int)ProfileIntegrityItems.NowArea] : 0;

            var result2 = userProfilesRepository.UpdateIntegrity(newUserProfiles.Userid, integrity);

            return newUserProfiles.Userid > 0;
        }
        /// <summary>
        /// 获取用户资料实体
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserProfiles GetUserProfiles(long userId)
        {
            return userProfilesRepository.GetUserProfiles(userId);
        }


        /// <summary>
        /// 编辑用户资料
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool EditUserProfiles(UserProfiles entity)
        {
            return userProfilesRepository.UpdateCache(entity);
        }
    }
}
