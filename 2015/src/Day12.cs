using System;
using System.Collections.Generic;
using System.Linq;
using _2015.tools;

namespace _2015
{
    public static class Day12
    {
        public static int SumIntegers(string input) => 
            input.ExtractIntegers().Sum();

        public static int Part2(string input)
        {
            var slicesToIgnore = SlicesToIgnore(input,'{', "red", '}').ToArray();
            var substringsExcept = input.SubstringsExcept(slicesToIgnore).ToArray();
            foreach (var slice in substringsExcept)
            {
                if(slice.Contains("red"))
                    Console.WriteLine(slice);
            }

            var reds = substringsExcept.Select(SumIntegers).Sum();
            var all = SumIntegers(input);
            return all-reds;
        }
            

        public static IEnumerable<(int, int)> SlicesToIgnore(string s, char chStart, string value, char chEnd)
        {
            var head = -1;
            Func<int,bool> isset = (h)=> h >= 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == chStart)
                {
                    if (!isset(head) ||(isset(head) && !s.Substring(head,i-head).Contains(value)))
                    {
                        head = i;
                        
                    }
                }

                if (s[i] == chEnd)
                {
                    if (isset(head) && s.Substring(head, i - head).Contains(value))
                    {
                        yield return (head, i);
                        head = -1;
                    }                    
                } 
            }
        }
    }
}