using System;
using Microsoft.AspNetCore.Mvc.Razor;

namespace SexyColor.CommonComponents
{
    public class ScriptRegister : ResourceRegister
    {
        public ScriptRegister(RazorPage page, Action<ResourceCollection> onRegisted) : base(page, onRegisted)
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
            if (!ResourceManager.ScriptSource.ContainsKey(name))
                throw new Exception("找不到名称为“{0}”的相关资源".FormatWith(name));
            return new ResourceCapture(CurrentPage, ResourceManager.ScriptSource[name], OnRegisted);
        }


    }
}
