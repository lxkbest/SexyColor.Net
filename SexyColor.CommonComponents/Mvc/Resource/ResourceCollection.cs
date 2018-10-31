using System.Collections.Generic;

namespace SexyColor.CommonComponents
{
    public class ResourceCollection : List<ResourceEntity>
    {
        public string Name { get; set; }
        public bool Required { get; set; }
        public ResourcePosition Position { get; set; }
    }
}
