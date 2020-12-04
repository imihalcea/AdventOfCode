using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using _2015.tools;

namespace _2015
{
    public static class Day12
    {
        public static int SumIntegers(string input) => 
            input.ExtractIntegers().Sum();

        public static int Part2(string input)
        {
            var withoutReds = Ignore(input,@"\{[^\{\}]*\:""red""[^\{\}]*\}");
            var all = SumIntegers(withoutReds);
            return all;
        }

        public static int Solve(string input, bool skipRed=true) {
            int Traverse(JsonElement t) {
                return t.ValueKind switch
                {
                    JsonValueKind.Object when skipRed && t.EnumerateObject().Any(
                        p => p.Value.ValueKind == JsonValueKind.String && p.Value.GetString() == "red") => 0,
                    JsonValueKind.Object => t.EnumerateObject().Select(p => Traverse(p.Value)).Sum(),
                    JsonValueKind.Array => t.EnumerateArray().Select(Traverse).Sum(),
                    JsonValueKind.Number => t.GetInt32(),
                    _ => 0
                };
            }

            return Traverse(JsonDocument.Parse(input).RootElement);
        }

        private static string Ignore(string s, string pattern)
        {
                var regExp = new Regex(pattern);
                s = regExp.Replace(s, string.Empty);
                while (regExp.IsMatch(s))
                {
                    s = regExp.Replace(s, string.Empty);
                }
            return s;
        }
    }
}