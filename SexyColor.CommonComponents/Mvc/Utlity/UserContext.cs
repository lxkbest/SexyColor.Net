using Microsoft.AspNetCore.Http;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using System;

namespace SexyColor.CommonComponents
{
    public class UserContext
    {

        public static User CurrentUser
        {
            get
            {
                if (HttpContextCore.Current == null)
                {
                    return null;
                }
                IUserService userService = DIContainer.Resolve<IUserService>();
                User currentUser = null;
                if (HttpContextCore.Current.User.Identity.IsAuthenticated)
                    currentUser = userService.GetFullUser(HttpContextCore.Current.User.Identity.Name);
                if (currentUser != null)
                    return currentUser;
                return null;
            }
            

            
        }
    }
}
