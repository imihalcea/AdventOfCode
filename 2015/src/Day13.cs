using System;
using System.Collections.Generic;
using System.Linq;
using _2015.tools;

namespace _2015
{
    public class Day13
    {
        public static int Part1(Dictionary<(string p1, string p2), int> dataSet)
        {
            var allPersons = dataSet.Keys.SelectMany(p => new[] {p.p1, p.p2}).ToHashSet();
            return allPersons.ToArray().Permutations().Select(ar =>
                {
                    ar = ar.Append(ar.First()).ToArray();
                    return ar.Pairwise((p1, p2) => dataSet[(p1, p2)] + dataSet[(p2,p1)]).Sum();
                })
                .Max();
        }
        
        
        public static int Part2(Dictionary<(string p1, string p2), int> dataSet)
        {
            var allPersons = dataSet.Keys.SelectMany(p => new[] {p.p1, p.p2}).ToHashSet();
            allPersons.ToList().ForEach(p=>
            {
                dataSet.Add(("Me",p),0);
                dataSet.Add((p,"Me"),0);
            });
            allPersons.Add("Me");
            return allPersons.ToArray().Permutations().Select(ar =>
                {
                    ar = ar.Append(ar.First()).ToArray();
                    return ar.Pairwise((p1, p2) => dataSet[(p1, p2)] + dataSet[(p2,p1)]).Sum();
                })
                .Max();
        }
    }
}