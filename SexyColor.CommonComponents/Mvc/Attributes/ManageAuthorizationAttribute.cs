using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Internal;
using SexyColor.BusinessComponents;


namespace SexyColor.CommonComponents.Mvc
{
    public class AuthenticationRequirement : AuthorizationHandler<AuthenticationRequirement>, IAuthorizationRequirement
    {
        public IUserService userService { get; set; }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthenticationRequirement requirement)
        {
            
            var user = context.User;
            var userIsAnonymous = user?.Identity == null || string.IsNullOrWhiteSpace(user.Identity.Name);
            if (!userIsAnonymous)
            {
                context.Succeed(requirement);
            }

            return TaskCache.CompletedTask;
        }
    }
}
