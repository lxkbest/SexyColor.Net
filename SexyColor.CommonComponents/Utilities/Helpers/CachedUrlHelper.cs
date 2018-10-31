using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;

namespace SexyColor.CommonComponents
{
    public class CachedUrlHelper
    {
        public static string Action(string actionName, string controllerName, string routeName)
        {
            return Action(actionName, controllerName, routeName, null);
        }

        public static string Action(string actionName, string controllerName, string routeName, RouteValueDictionary routeValueDictionary)
        {
            string cacheKey = string.Format("ActionUrl::c:{0}-a:{1}-r:{2}", controllerName, actionName, routeName);

            RouteValueDictionary routeParameters = new RouteValueDictionary();
            string[] values = null;
            if (routeValueDictionary != null)
            {
                values = new string[routeValueDictionary.Count];
                int index = 0;
                foreach (KeyValuePair<string, object> pair in routeValueDictionary)
                {
                    cacheKey += "-" + pair.Key + ":{" + index + "}";

                    if (pair.Value == null)
                        values[index] = string.Empty;
                    else
                        values[index] = pair.Value.ToString();

                    routeParameters[pair.Key] = "{" + index + "}";

                    index++;
                }
            }

            ICacheService cacheService = DIContainer.Resolve<ICacheService>();
            string url = cacheService.Get<string>(cacheKey);
            if (url == null)
            {

                try
                {
                    var httpContext = HttpContextCore.Current;
                    var actionContext = new ActionContext(httpContext, httpContext.GetRouteData(), new ActionDescriptor());
                    url = UrlHelperExtensions.Action(new UrlHelper(actionContext), actionName, controllerName, routeParameters);
                }
                catch (Exception e)
                {
                    url = string.Empty;
                }

                if (string.IsNullOrEmpty(url))
                    return string.Empty;
                url = url.Replace("%7b", "{").Replace("%7d", "}").Replace("%7B", "{").Replace("%7D", "}");//.ToLower();
                cacheService.Add(cacheKey, url, CachingExpirationType.Stable);
            }

            if (values != null)
                return string.Format(url, values);
            else
                return url;
        }
    }
}
