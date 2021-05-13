using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using MS.SVGReader.Main.Models;
using MS.SVGReader.Main.Models.Internal;
using MS.SVGReader.Main.Utils;

namespace MS.SVGReader.Main.Styles
{
    internal class SVGStyleEntityParser<T> where T : XNode
    {
        private readonly string _idPrefix;
        private readonly string _contentPrefix;
        private readonly string _contentEnding;
        private readonly string _regexSplitter;

        private readonly Func<T, string> _valueExtractor;
        
        public SVGStyleEntityParser(Func<T, string> valueExtractor,
            string regexSplitter,
            string idPrefix, 
            string contentPrefix, 
            string contentEnding)
        {
            _idPrefix = idPrefix;
            _contentPrefix = contentPrefix;
            _contentEnding = contentEnding;
            _valueExtractor = valueExtractor;
            _regexSplitter = regexSplitter;
        }

        public SVGStyle Parse(T documentType)
        {
            var rawValue = _valueExtractor(documentType);
            var style = new SVGStyle();
            foreach (var tuple in SelectProperties(rawValue))
                style.Add(tuple.Item1, tuple.Item2);
            return style;
        }

        private IEnumerable<Tuple<string, StyleProperties>> SelectProperties(string rawValue) =>
            SVGUtils
                .SelectClasses(regex: _regexSplitter, rawValue: rawValue)
                .SelectMany(cls => SVGUtils.CreateStylePropertiesWithID(
                    classProperties: cls,
                    idPrefix: _idPrefix,
                    contentEnding: _contentEnding,
                    contentPrefix: _contentPrefix));
    }
}