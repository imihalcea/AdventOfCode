using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace _2015
{
    public static class Day8
    {
        
        public static int Part1(IEnumerable<string> dataSet1) =>
         dataSet1.Select(str => str.Length-str.UnEscLen()).Sum();

        public static int Part2(IEnumerable<string> dataSet1) =>
            dataSet1.Select(str => str.EscLen()-str.Length).Sum();

        private static int UnEscLen(this  string line)
        {
            var replace = line.Substring(1, line.Length - 2).Replace("\\\"", "\"").Replace("\\\\", "?");
            return Regex.Replace(replace, @"\\x[0-9a-f]{2}", "?").Length;
        }

        private static int EscLen(this string line) => 
            ("\"" + line.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"").Length;
    }
}