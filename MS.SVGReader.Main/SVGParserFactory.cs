using MS.SVGReader.Main.Models;
using MS.SVGReader.Main.Parser;

namespace MS.SVGReader.Main
{
    /// <summary>
    /// Creates a svg file parser.
    /// <seealso cref="ISVGParser"/>
    /// </summary>
    public static class SVGParserFactory
    {
        public static ISVGParser Create()
            => new SVGXDocumentParser<SVGFile>();
    }
}