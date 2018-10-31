using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SexyColor.BusinessComponents;

namespace SexyColor.CommonComponents
{
    public abstract class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.Name == null)
            {
                context.Result = new RedirectToRouteResult("default", new { controller = "System", action = "ManageLogin" });
                base.OnActionExecuting(context);
                return;
            }
            var userId = UserIdToUserNameDictionary.GetUserId(context.HttpContext.User.Identity.Name);
            var user = context.HttpContext.Session.Get<User>(userId.ToString());

            if (!user.IsAllowEntryManager())
            {
                context.Result = new RedirectToRouteResult("Error", new { controller = "Error", action = "SystemError", errorMsg = "您目前没有权限登录管理后台" });
            }
            base.OnActionExecuting(context);
        }
    }
}
