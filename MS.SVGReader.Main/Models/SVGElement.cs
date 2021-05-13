namespace MS.SVGReader.Main.Models
{
    /// <summary>
    /// Representation of a svg path tag.
    /// <example>
    ///     <path d="M450.2,579.9L450.2,579.9L450.2,579.9z"/>
    /// </example>
    /// </summary>
    public class SVGElement
    {
        public StyleProperties StyleProperties;
        
        /// <summary>
        /// Represents a 'd' attribute.
        /// </summary>
        public string Path;
    }
}