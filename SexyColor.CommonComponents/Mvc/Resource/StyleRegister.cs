using System;
using Microsoft.AspNetCore.Mvc.Razor;


namespace SexyColor.CommonComponents
{
    public class StyleRegister : ResourceRegister
    {
        public StyleRegister(RazorPage page, Action<ResourceCollection> onRegisted) : base(page, onRegisted)
        {
        }

        public override IDisposable AtHead()
        {
            return new Capture(this.CurrentPage, ResourcePosition.Head, OnRegisted);
        }

        public override IDisposable AtFoot()
        {
            return new Capture(this.CurrentPage, ResourcePosition.Foot, OnRegisted);
        }

        public override ResourceCapture Reqiured(string name)
        {
            return new ResourceCapture(this.CurrentPage, ResourceManager.StyleSource[name], OnRegisted);
        }
    }
}
