using SexyColor.BusinessComponents;

namespace SexyColor.CommonComponents
{
    public class AuthorizerHelper
    {
        public IUserService userService { get; set; }
        public IPermissionItemsService permissionItemsService { get; set; }

        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        /// <returns></returns>
        public bool IsSuperAdministrator(User user)
        {
            return user.IsSuperAdministrator();
        }

        /// <summary>
        /// 是不是内容管理员
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsContentAdministrator(User user)
        {
            return user.IsContentAdministrator();
        }

        /// <summary>
        /// 是否具有管理此用户的权限
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public bool IsUserPower(long userId)
        {
            if (userId <= 0)
                return false;

            User user = userService.GetUser(userId);
            if (user == null)
                return false;

            User currentUser = UserContext.CurrentUser;
            if (currentUser == null)
                return false;

            var founderId = BuilderContext.Configuration["UseFounder"];
            long resultId = 0;
            if (!string.IsNullOrEmpty(founderId) && long.TryParse(founderId, out resultId))
                if (resultId == currentUser.UserId && resultId != 0)
                    return true;

            if (IsSuperAdministrator(currentUser) && resultId != userId && !IsSuperAdministrator(user))
                return true;

            if (IsSuperAdministrator(currentUser) && resultId != userId && !IsSuperAdministrator(user))
                return true;


            return true;
        }

        /// <summary>
        /// 检验是否可以访问
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public bool Check(User currentUser, string itemKey)
        {
            if (currentUser == null)
                return false;

            if (IsSuperAdministrator(currentUser))
                return true;

            if (IsContentAdministrator(currentUser))
                return true;

            PermissionItems item =  permissionItemsService.GetPermissionItemsByItemkey(itemKey);
            string[] urlArray = item.ItemUrl.Split('/');

            return PermissionsCollction.PermissionItemsCollction.TryGetValue(urlArray[urlArray.Length - 1], out var result); 
        }
    }
}
