﻿using System.Collections.Generic;

namespace SexyColor.CommonComponents
{
    public class ResourceHelper
    {
        Dictionary<string, ResourceCollection> _source;
        ResourceType _resourceType;
        private string _name;
        public ResourceHelper(string name, ResourceType resourceType)
        {
            switch (resourceType)
            {
                case ResourceType.Script:
                    _source = ResourceManager.ScriptSource;
                    break;
                case ResourceType.Style:
                    _source = ResourceManager.StyleSource;
                    break;
            }
            _source.Add(name, new ResourceCollection { Name = name });
            _name = name;
            _resourceType = resourceType;
        }
        public ResourceHelper Include(string source)
        {
            _source[_name].Add(new ResourceEntity { SourceType = _resourceType, DebugSource = source, ReleaseSource = source });
            return this;
        }
        public ResourceHelper Include(string debuger, string release)
        {
            _source[_name].Add(new ResourceEntity { SourceType = _resourceType, DebugSource = debuger, ReleaseSource = release });
            return this;
        }
        public ResourceHelper Include(string debuger, string release, string cdn)
        {
            _source[_name].Add(new ResourceEntity { SourceType = _resourceType, DebugSource = debuger, CDNSource = cdn, ReleaseSource = release });
            return this;
        }
        public ResourceHelper RequiredAtHead()
        {
            _source[_name].Required = true;
            _source[_name].Position = ResourcePosition.Head;
            return this;
        }
        public ResourceHelper RequiredAtFoot()
        {
            _source[_name].Required = true;
            _source[_name].Position = ResourcePosition.Foot;
            return this;
        }
    }
}
