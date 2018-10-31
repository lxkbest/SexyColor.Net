using Microsoft.AspNetCore.Mvc.Razor;
using System;

namespace SexyColor.CommonComponents
{
    public class ResourceCapture
    {
        private RazorPage _page;
        private ResourceCollection _resource;
        private Action<ResourceCollection> _onRegisted;
        public ResourceCapture(RazorPage page, ResourceCollection source, Action<ResourceCollection> onRegisted)
        {
            _page = page;
            _resource = source;
            _onRegisted = onRegisted;
        }
        public void AtHead()
        {
            var resources = new ResourceCollection();
            resources.Name = _resource.Name;
            resources.Position = ResourcePosition.Head;
            _resource.Each(m =>
            {
                ResourceEntity entity = m.ToNew();
                entity.Position = ResourcePosition.Head;
                resources.Add(entity);
            });
            _onRegisted(resources);
        }
        public void AtFoot()
        {
            var resources = new ResourceCollection();
            resources.Name = _resource.Name;
            resources.Position = ResourcePosition.Foot;
            _resource.Each(m =>
            {
                ResourceEntity entity = m.ToNew();
                entity.Position = ResourcePosition.Foot;
                resources.Add(entity);
            });
            _onRegisted(resources);
        }
    }
}
