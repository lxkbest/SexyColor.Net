using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using SexyColor.Infrastructure;

namespace SexyColor.CommonComponents
{
    public class HandleErrorToLogAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            DIContainer.Resolve<ILogger>().Error(filterContext.Exception);
            //filterContext.HttpContext.RequestServices.GetService<ILogger>().Error(filterContext.Exception);
        }
    }
}
