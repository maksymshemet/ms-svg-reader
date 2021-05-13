using System.Collections.Generic;

namespace MS.SVGReader.Main.Models
{
    /// <summary>
    /// Representation of a svg file.
    /// </summary>
    public class SVGFile
    {
        public string FilePath;
        public string FileName;
        /// <summary>
        /// List of path tag representations with styles.
        /// <see cref="SVGElement"/>
        /// </summary>
        public List<SVGElement> Elements;
    }
}