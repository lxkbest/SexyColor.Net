using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using SexyColor.BusinessComponents;

namespace SexyColor.CommonComponents
{
    public class EnterSystemRequirement : AuthorizationHandler<EnterSystemRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EnterSystemRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                return Task.CompletedTask;
            }
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Name))
            {
                return Task.CompletedTask;
            }
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Sid))
            {
                return Task.CompletedTask;
            }
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.Sid).Value;
            var mvcContext =  context.Resource as AuthorizationFilterContext;
            var user =  mvcContext.HttpContext.Session.Get<User>(userId);
            if (user != null && user.UserId > 0 && user.UserName.Equals(context.User.Identity.Name))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
