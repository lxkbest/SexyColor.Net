using Microsoft.AspNetCore.Mvc.Razor;
using System;

namespace SexyColor.CommonComponents
{
    public abstract class ResourceRegister
    {
        public RazorPage CurrentPage { get; private set; }

        protected ResourceRegister(RazorPage page, Action<ResourceCollection> onRegisted)
        {
            this.CurrentPage = page;
            this.OnRegisted = onRegisted;
        }

        public abstract IDisposable AtHead();

        public abstract IDisposable AtFoot();

        public abstract ResourceCapture Reqiured(string name);

        public Action<ResourceCollection> OnRegisted { get; set; }
    }
}
