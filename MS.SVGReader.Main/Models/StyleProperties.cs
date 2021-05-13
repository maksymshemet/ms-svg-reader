using System.Collections.Generic;

namespace MS.SVGReader.Main.Models
{
    /// <summary>
    /// Representation of a style(like fill, stroke..etc) properties of path tag.
    /// It could be provided as a css style or tag attributes
    /// 
    /// <example>
    /// 1. <style type="text/css">
    ///        .st9{fill:#FFFFFF;stroke:#010101;stroke-miterlimit:10;}
    ///    </style>
    ///    <path class="st9" d="M450.2,579.9L450.2,579.9L450.2,579.9z"/> 
    /// 2. <path style="fill:#F6EB16;"  d="M450.2,579.9L450.2,579.9L450.2,579.9z" />
    /// </example>
    /// 
    /// <example>
    /// <path fill="#FFFFFF" stroke="#010101" stroke-miterlimit="10"
    ///          d="M450.2,579.9L450.2,579.9L450.2,579.9z"/>
    /// </example
    /// </summary>
    public class StyleProperties
    {
        public int Count => _props.Count;
        
        private readonly Dictionary<string, string> _props;

        public StyleProperties() => 
            _props = new Dictionary<string, string>();

        public void Clear() => 
            _props.Clear();
        
        public string Get(string key) =>
            _props.TryGetValue(key, out var val) ? val : null;
        
        public string Add(string key, string val) =>
            _props[key] = val;
        
        public string this[string key]
        {
            get => Get(key);
            set => Add(key, value);
        }
        
        public override string ToString() => 
            $"StyleProperties: count={_props.Count}";
    }
}