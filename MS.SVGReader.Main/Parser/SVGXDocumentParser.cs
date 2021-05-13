using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using MS.SVGReader.Main.Models;
using MS.SVGReader.Main.Models.Internal;
using MS.SVGReader.Main.Styles;
using MS.SVGReader.Main.Utils;

namespace MS.SVGReader.Main.Parser
{
    internal class SVGXDocumentParser<T> : ISVGParser
    {
        private readonly SVGStyleFactory _styleFactory;

        public SVGXDocumentParser() =>
            _styleFactory = new SVGStyleFactory();
        
        public SVGFile ParseFile(string path)
        {
            var document = XDocument.Load(path);

            var style = LoadStyle(document.DescendantNodes());
            var elements = document.DescendantNodes()
                .Where(x => x is XElement)
                .Cast<XElement>()
                .Where(x => x.Name.LocalName == "path")
                .Where(x => x.HasAttributes)
                .Select(x => CreateSvgDta(x, style))
                .ToList();

            return new SVGFile
            {
                FilePath = path,
                FileName = Path.GetFileName(path),
                Elements = elements
            };
        }

        private SVGStyle LoadStyle(in IEnumerable<XNode> nodes) =>
            nodes
                .Select(node => _styleFactory.CreateFrom(node))
                .FirstOrDefault(style => style != null);
        
        private static SVGElement CreateSvgDta(XElement element, SVGStyle style)
        {
            if (MissingPathAttribute(element)) return null;

            var d = element.Attributes().First(x => x.Name.LocalName == "d").Value;
            var styleProps = CreateStyleProperties(element, style);

            return new SVGElement
            {
                StyleProperties = styleProps,
                Path = d
            };
        }

        private static bool MissingPathAttribute(XElement element) => 
            !element.HasAttributes || element.Attributes().All(IsNotPathAttribute);

        private static bool IsNotPathAttribute(XAttribute attribute) =>
            attribute.Name.LocalName != "d";
        
        private static StyleProperties CreateStyleProperties(XElement element, SVGStyle style)
        {
            var attributes = element.Attributes();
            
            XAttribute attribute;
            if (TryGetClassAttribute(attributes, out attribute))
                return style[attribute.Value];

            if (TryGetStyleAttribute(attributes, out attribute))
                return attribute.Value.StartsWith("&")
                    ? style[attribute.Value]
                    : SVGUtils.ParseStyleContent(attribute.Value);
            
            var styleProps = new StyleProperties();
            foreach (var attr in attributes
                .Where(IsNotPathAttribute))
                styleProps[attr.Name.LocalName] = attr.Value;

            return styleProps;
        }

        private static bool TryGetClassAttribute(IEnumerable<XAttribute> attributes, out XAttribute attr)
        {
            attr = attributes.FirstOrDefault(x => x.Name.LocalName == "class");
            return attr != null;
        }
        
        private static bool TryGetStyleAttribute(IEnumerable<XAttribute> attributes, out XAttribute attr)
        {
            attr = attributes.FirstOrDefault(x => x.Name.LocalName == "style");
            return attr != null;
        }
    }
}