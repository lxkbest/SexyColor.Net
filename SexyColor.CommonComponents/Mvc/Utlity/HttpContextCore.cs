using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.CommonComponents
{
    public static class HttpContextCore
    {
        public static IServiceProvider ServiceProvider;

        static HttpContextCore()
        { }

        public static HttpContext Current
        {
            get
            {
                object factory = ServiceProvider.GetService(typeof(IHttpContextAccessor));
                HttpContext context = ((IHttpContextAccessor)factory).HttpContext;
                return context;
            }
        }

    }
}
