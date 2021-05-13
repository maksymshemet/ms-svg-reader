using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MS.SVGReader.Main.Models;

namespace MS.SVGReader.Main.Utils
{
    internal static class SVGUtils
    {
        internal static IEnumerable<string> SelectClasses(string regex, string rawValue)
        {
            var collection = Regex.Matches(rawValue, regex);
            for (var i = 0; i < collection.Count; i++)
            {
                var rindex = 0;
                var lindex = collection[i].Index;
                if (i + 1 < collection.Count)
                {
                    rindex = collection[i + 1].Index;
                }
                else
                {
                    rindex = rawValue.Length - 1;
                }

                yield return rawValue.Substring(
                    startIndex: lindex, 
                    length: rindex - lindex).Trim();
            }
        }
        
        internal static IEnumerable<Tuple<string, StyleProperties>> CreateStylePropertiesWithID(
            string idPrefix,
            string contentPrefix,
            string contentEnding,
            string classProperties)
        {

            var idPrefixLength = idPrefix.Length;
            var contentPrefixLength = contentPrefix.Length;
            var contentEndingLength = contentEnding.Length;
            
            var indexIdPrefix = classProperties.IndexOf(idPrefix, StringComparison.Ordinal);
            var indexContentPrefix = classProperties.IndexOf(contentPrefix, StringComparison.Ordinal);
            var indexContentEnding = classProperties.LastIndexOf(contentEnding, StringComparison.Ordinal);

            var id = classProperties.Substring(
                startIndex: indexIdPrefix + idPrefixLength, 
                length: indexContentPrefix - idPrefixLength).Trim();
            var content = classProperties.Substring(
                startIndex: indexContentPrefix + contentPrefixLength, 
                length: indexContentEnding - indexContentPrefix - contentEndingLength);

            yield return Tuple.Create(id, ParseStyleContent(content));
        }

        internal static StyleProperties ParseStyleContent(string content)
        {
            var sp = new StyleProperties();

            foreach (var prop in Regex.Split(content, ";")
                .Where(srt => !string.IsNullOrWhiteSpace(srt)))
            {
                
                var nameValue = Regex.Split(prop, ":");
                sp[nameValue[0].Trim()] = nameValue[1].Trim();
            }

            return sp;
        }
    }
}