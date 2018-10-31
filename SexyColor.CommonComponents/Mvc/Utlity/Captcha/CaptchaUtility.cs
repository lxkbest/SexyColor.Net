using Microsoft.AspNetCore.Http;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;

namespace SexyColor.CommonComponents
{
    /// <summary>
    /// 验证码管理类
    /// </summary>
    public static class CaptchaUtility
    {
        private static ICacheService cacheService = DIContainer.Resolve<ICacheService>();

        /// <summary>
        /// 是否开始使用验证码
        /// </summary>
        /// <returns></returns>
        public static bool UseCaptcha(VerifyScenarios scenarios = VerifyScenarios.Post, bool isLimitCount = false)
        {
            //验证码配置类获取配置是否启用验证码
            var catchaSettings = CaptchaSettings.Instance();
            if (!catchaSettings.EnableCaptcha)
                return false;
            int? limitTryCount = null;
            User currentUser = UserContext.CurrentUser;
            if (scenarios == VerifyScenarios.Register)
                return true;
            if (scenarios == VerifyScenarios.Login && currentUser != null)
                return true;


            string userName = GetUserName(HttpContextCore.Current);
            string cacheKey = GetLimitTryCount(userName, scenarios);
            limitTryCount = cacheService.Get(cacheKey) as int?;
             

            //登录的时候输错三次密码，就使用验证码
            if (limitTryCount.HasValue && ((scenarios == VerifyScenarios.Login && limitTryCount >= catchaSettings.CaptchaLoginCount)))
                return true;
            
            //是否累计次数
            if (isLimitCount)
            {
                if (limitTryCount.HasValue)
                    limitTryCount++;
                else
                    limitTryCount = 1;
                if (limitTryCount == 1)
                    cacheService.Add(cacheKey, limitTryCount, CachingExpirationType.SingleObject);
                else
                    cacheService.Set(cacheKey, limitTryCount, CachingExpirationType.SingleObject);
            }
            return false;
        }


        /// <summary>
        /// 重置累计次数，具体是否使用按上面规则实施
        /// </summary>
        public static void ResetLimitTryCount(VerifyScenarios scenarios)
        {
            var httpContext = HttpContextCore.Current;
            string cacheKey = GetLimitTryCount(GetUserName(httpContext), scenarios);
            int? limitTryCount = cacheService.Get(cacheKey) as int?;
            if (limitTryCount.HasValue)
                cacheService.Remove(cacheKey);
        }
        /// <summary>
        /// 获取用户名称
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private static string GetUserName(HttpContext httpContext)
        {

            string userName = string.Empty;
            User currentUser = GetCurrentUser(httpContext);

            if (currentUser != null)
                userName = currentUser.UserName;
            //if (httpContext != null)
            //{
            //     if (httpContext.Request.Form != null && !string.IsNullOrEmpty(httpContext.Request.Form["UserName"]))
            //        userName = httpContext.Request.Form["UserName"];
            //}
            userName = GetIP();
            return userName;
        }

        public static string GetIP()
        {
            string result = (HttpContextCore.Current.Request.Headers["HTTP_X_FORWARDED_FOR"].ToString() != null
            && HttpContextCore.Current.Request.Headers["HTTP_X_FORWARDED_FOR"] != string.Empty)
            ? HttpContextCore.Current.Request.Headers["HTTP_X_FORWARDED_FOR"]
            : HttpContextCore.Current.Request.Headers["REMOTE_ADDR"];
 
            if (string.IsNullOrEmpty(result))
                result = HttpContextCore.Current.Request.Headers["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(result))
                return "127.0.0.1";

            return result;
 
        }

        private static User GetCurrentUser(HttpContext httpContext)
        {
            if (httpContext != null && !string.IsNullOrEmpty(httpContext.User.Identity.Name)) 
            {
                IUserService userService = DIContainer.Resolve<IUserService>();
                User currentUser = userService.GetFullUser(httpContext.User.Identity.Name);
                return currentUser;
            }
            return null;
        }

        private static string GetLimitTryCount(string userName, VerifyScenarios scenarios)
        {
            return string.Format("LimitTryCount::UserName-{0}:Scenarios-{1}", userName, scenarios);
        }
    }
}
