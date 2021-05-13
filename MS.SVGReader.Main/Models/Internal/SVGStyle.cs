using System.Collections.Generic;

namespace MS.SVGReader.Main.Models.Internal
{
    internal class SVGStyle
    {
        public bool IsEmpty => _props.Count == 0;
        
        private readonly Dictionary<string, StyleProperties> _props;

        public SVGStyle() => 
            _props = new Dictionary<string, StyleProperties>();

        public void Add(string id, StyleProperties properties) => 
            _props[id] = properties;

        public StyleProperties this[string key]
        {
            get => _props.TryGetValue(key, out var val) ? val : null;
            set => _props[key] = value;
        }

        public override string ToString() => 
            $"SvgStyle: count={_props.Count}";
    }
}