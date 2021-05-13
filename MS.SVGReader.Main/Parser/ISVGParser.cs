using MS.SVGReader.Main.Models;

namespace MS.SVGReader.Main.Parser
{

    public interface ISVGParser
    {
        /// <summary>
        ///  Parses svf file and return its representation.
        /// </summary>
        /// <param name="path">path to file</param>
        /// <returns>
        /// <see cref="SVGFile" />
        /// </returns>
        SVGFile ParseFile(string path);
    }
}