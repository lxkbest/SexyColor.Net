using System;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;

namespace SexyColor.CommonComponents
{
    public class Capture : IDisposable
    {
        private RazorPage _page;
        private ResourcePosition _position;
        private Action<ResourceCollection> _onRegisted;

        public Capture(RazorPage page, ResourcePosition position, Action<ResourceCollection> onRegisted)
        {
            this._page = page;
            _position = position;
            this._page.StartTagHelperWritingScope(HtmlEncoder.Default);
            _onRegisted = onRegisted;
        }

        public void Dispose()
        {
            var content = this._page.EndTagHelperWritingScope();
            var partResult = new HtmlString(content.GetContent());

            var resource = new ResourceCollection
                {
                    new ResourceEntity { Position = _position, Source = partResult }
                };
            resource.Position = _position;
            _onRegisted(resource);
        }
    }
}
