using System;
using System.Xml.Linq;
using MS.SVGReader.Main.Models.Internal;

namespace MS.SVGReader.Main.Styles
{
    internal class SVGStyleFactory
    {
        public SVGStyle CreateFrom(in XNode xnode)
        {
            if (xnode is XElement element &&
                string.Equals(element.Name.LocalName, "style", StringComparison.OrdinalIgnoreCase))
            {
                return new SVGStyleEntityParser<XElement>(
                    valueExtractor: el => el.Value,
                    regexSplitter: @"\..+{",
                    idPrefix: ".",
                    contentPrefix: "{",
                    contentEnding: "}"
                ).Parse(element);
            }

            if (xnode is XDocumentType documentType && documentType.InternalSubset.Contains("!ENTITY"))
            {
                return new SVGStyleEntityParser<XDocumentType>(
                    valueExtractor: dt => dt.InternalSubset,
                    regexSplitter: "!ENTITY",
                    idPrefix: "!ENTITY",
                    contentPrefix: "\"",
                    contentEnding: "\""
                ).Parse(documentType);
            }
            return null;
        }
    }
}