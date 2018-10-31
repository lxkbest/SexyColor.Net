using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace SexyColor.CommonComponents
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class OperationLogsAttribute : ActionFilterAttribute
    {
        private IUserService userService = DIContainer.Resolve<IUserService>();
        private IPermissionItemsService permissionItemsService = DIContainer.Resolve<IPermissionItemsService>();
        private OperationLogsService operationLogsService = DIContainer.Resolve<OperationLogsService>();

        public bool IsTrusteeship { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public PermissionItemsType Type { get; set; }

        public OperationLogsAttribute(bool trusteeship)
        {
            IsTrusteeship = trusteeship;
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            var userId = context.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Sid).Value;
            var currentUser = context.HttpContext.Session.Get<User>(userId);
            if (currentUser == null)
                currentUser = userService.GetFullUser(HttpContextCore.Current.User.Identity.Name);
            if (currentUser == null)
                base.OnResultExecuted(context);

            string controller = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["action"].ToString();
            string url = RequestHelper.RawUrl(context.HttpContext.Request);

            OperationLogs logs = new OperationLogs();
            logs.Source = IsTrusteeship ? "系统设定" : "权限设定";
            logs.Description = string.IsNullOrEmpty(Description) ? string.Empty : Description;

            


            logs.DateCreated = DateTime.Now;
            logs.OperatorIp = Utility.GetIP();
            logs.OperatorUrl = url;
            logs.OperatorUserid = currentUser.UserId;
            logs.OperatorUsername = currentUser.UserName;


            IEnumerable<PermissionItems> permissionItemsList = permissionItemsService.GetPermissionItemsAll();
            if (!IsTrusteeship)
            {
                foreach (var item in permissionItemsList)
                {
                    if (item.ItemUrl.Equals(context.HttpContext.Request.Path.ToString().Replace(".aspx", ""), StringComparison.CurrentCultureIgnoreCase))
                    {
                        logs.ApplicationId = item.Applicationid;
                        logs.OperationObjectId = item.Itemkey;
                        logs.OperationObjectName = item.Itemname;
                        logs.OperationType = Enum.Parse(typeof(PermissionItemsType), item.ItemType.ToString()).ToString();
                        break;
                    }
                }
                if (logs.OperationObjectId.IsNullOrWhiteSpace())
                {
                    logs.OperationObjectId = logs.OperationObjectName = logs.OperationType = string.Empty;
                    logs.ApplicationId = 0;
                }
            }
            else
            {
                logs.ApplicationId = 0;
                logs.OperationObjectId = string.Empty;
                logs.OperationObjectName = Name;
                logs.OperationType = Type.ToString();
            }

            
            operationLogsService.AddOperationLogs(logs);

            base.OnResultExecuted(context);
        }
    }
}
