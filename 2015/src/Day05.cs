using System;
using System.Collections.Generic;
using System.Linq;
using static System.Linq.Enumerable;

namespace _2015
{
    public static class Day5
    {
        public static bool IsNice1(string input)
        {
            var disallowedNotPresent = new[] {"ab", "cd", "pq", "xy"}.All(d => !input.Contains(d));
            var atLeast3Vowels = input.Count(c=>"aeiou".Contains(c)) >= 3;
            var repeatingChars = Range(0,input.Length-1).Count(i=>input[i]==input[i+1])>0;
            return disallowedNotPresent && atLeast3Vowels && repeatingChars;
        }
        
        public static bool IsNice2(string input)
        {
            var pairGroups = Range(0, input.Length - 1)
                .Select(i=>(string.Concat(input[i],input[i+1]),new []{i, i+1}))
                .GroupBy(p=>p.Item1).ToArray();
            var pairTwiceNotOverlapping = pairGroups.Any(grp=>grp.SelectMany(g=>g.Item2).Distinct().Count()>=4);
            var letterRepeats = Range(0, input.Length - 2)
                .Any(i => input[i] == input[i + 2]);
            return pairTwiceNotOverlapping && letterRepeats;
        }

        public static int HowManyAreNice(IEnumerable<string> dataSet, Predicate<string> predicate) => 
            dataSet.Count(item=>predicate(item));


        
    }
}