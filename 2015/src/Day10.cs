using System;
using System.Collections.Generic;
using System.Linq;
using _2015.tools;

namespace _2015
{
    public class Day10
    {
        public static IEnumerable<int[]> LookAndSaySeq(string start)
        {
            var startArr = start.Select(c=>(c-'0')).ToArray();
            while (true)
            {
                startArr = startArr.GroupConsecutiveRepeats(c => c).SelectMany(g => new [] {g.values!.Count(),g.key}).ToArray();
                yield return startArr;
            }
        }
        
    }
}