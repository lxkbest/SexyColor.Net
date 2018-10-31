using Microsoft.AspNetCore.Mvc;
using SexyColor.BusinessComponents;
using SexyColor.CommonComponents;
using SexyColor.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SexyColor.Web
{
    public class LeftMenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            User user = UserContext.CurrentUser;
            LeftMenuModel model = new LeftMenuModel();
            var rolesService = DIContainer.Resolve<RolesService>();
            var permissionItemsService = DIContainer.Resolve<IPermissionItemsService>();
            model.IsSuperAdministrator = user.IsSuperAdministrator();
            model.IsContentAdministrator = user.IsContentAdministrator();
            IEnumerable<string> rolesNameList = rolesService.GetRolesNamesInUser(user.UserId);
            model.PermissionItemsList = await Task.FromResult(permissionItemsService.GetPermissionItemsAll());
            model.PermissionItemsInRolesList = await Task.FromResult(permissionItemsService.Merge(permissionItemsService.GetPermissionItemsInRolesByRolesname(rolesNameList)));
            return View(model);
        }
    }
}
