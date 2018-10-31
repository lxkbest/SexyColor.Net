using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SexyColor.CommonComponents
{
    public class PermissionsAttribute : ActionFilterAttribute
    {
        private IUserService userService = DIContainer.Resolve<IUserService>();
        private RolesService rolesService = DIContainer.Resolve<RolesService>();
        private IPermissionItemsService permissionItemsService = DIContainer.Resolve<IPermissionItemsService>();
        private AuthorizerHelper authorizerHelper = DIContainer.Resolve<AuthorizerHelper>();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Sid).Value;
            var currentUser = context.HttpContext.Session.Get<User>(userId);
            if (currentUser == null)
                currentUser = userService.GetFullUser(HttpContextCore.Current.User.Identity.Name);
            if (currentUser == null)
            {
                context.Result = new RedirectToRouteResult("Default", new { controller = "System", action = "ManageLogin" });
                base.OnActionExecuting(context);
            }

            string controller = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["action"].ToString();
            string routeUrl = string.Empty;
            foreach (var route in context.RouteData.Routers)
            {
                if (route.GetType() == typeof(Route))
                {
                    routeUrl = ((Route)route).ParsedTemplate.TemplateText.Replace("{action}", action).Replace(".aspx", "");
                    routeUrl = "/" + routeUrl;
                    break;
                }
            }

            var isAllowed = IsAllowed(currentUser, controller, action, routeUrl);
            if (!isAllowed)
                context.Result = new RedirectToRouteResult("Error", new { controller = "Error", action = "SystemError" });
            base.OnActionExecuting(context);
        }

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var userId = context.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Sid).Value;
            var currentUser = context.HttpContext.Session.Get<User>(userId);
            if (currentUser == null)
                currentUser = userService.GetFullUser(HttpContextCore.Current.User.Identity.Name);
            if (currentUser == null)
            {
                context.Result = new RedirectToRouteResult("Default", new { controller = "System", action = "ManageLogin" });
                base.OnResultExecutionAsync(context, next);
            }

            if (PermissionsCollction.PermissionItemsCollction == null)
            {
                IEnumerable<PermissionItems> permissionItemsList = permissionItemsService.GetPermissionItemsAll();
                IEnumerable<string> rolesNameList = rolesService.GetRolesNamesInUser(currentUser.UserId);
                IEnumerable<string> permissionItemsInRolesList = permissionItemsService.Merge(permissionItemsService.GetPermissionItemsInRolesByRolesname(rolesNameList));
                var isSuperManage = false;
                if (authorizerHelper.IsSuperAdministrator(currentUser) || authorizerHelper.IsContentAdministrator(currentUser))
                    isSuperManage = true;

                PermissionsCollction.InitCollction(permissionItemsInRolesList, permissionItemsList, isSuperManage);
            }
            return base.OnResultExecutionAsync(context, next);
        }


        private bool IsAllowed(User user, string controller, string action, string routeUrl)
        {
            if (authorizerHelper.IsSuperAdministrator(user) || authorizerHelper.IsContentAdministrator(user))
                return true;

            IEnumerable<PermissionItems> permissionItemsList = permissionItemsService.GetPermissionItemsAll();
            PermissionItems item = permissionItemsList.FirstOrDefault(w => w.ItemUrl.Equals(routeUrl));
            if (item == null)
                return true;

            IEnumerable<string> rolesNameList = rolesService.GetRolesNamesInUser(user.UserId);
            if (rolesNameList.Count() <= 0)
                return false;

            IEnumerable<string> permissionItemsInRolesList = permissionItemsService.Merge(permissionItemsService.GetPermissionItemsInRolesByRolesname(rolesNameList));
            if (permissionItemsInRolesList != null && permissionItemsInRolesList.Contains(item.Itemkey))
                return true;

            return false;
        }
    }
}
