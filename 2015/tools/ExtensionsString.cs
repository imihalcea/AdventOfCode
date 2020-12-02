using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2015.tools
{
    public static class ExtensionsString
    {
        public static IEnumerable<int> ExtractIntegers(this string @this)
        {
            var regexp = new Regex(@"[\-0-9]+");
            foreach (Match match in regexp.Matches(@this))
            {
                yield return int.Parse(match.Value);
            }
        }

        public static IEnumerable<string> SubstringsExcept(this string @this, (int start, int end)[] ignoredSubstrings)
        {
            int nextStart = 0;
            var first = ignoredSubstrings.First();
            if (first.start > 0)
            {
                yield return @this.Substring(0, first.start);
            }
            
            foreach (var ignored in ignoredSubstrings)
            {
                    yield return @this.Substring(ignored.start, ignored.end - ignored.start);
            }
            
            var last = ignoredSubstrings.Last();
            if(last.end<@this.Length)
                yield return @this.Substring(last.end, @this.Length - last.end);
            
            
        }
    }
}