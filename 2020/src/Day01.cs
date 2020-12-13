using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace _2020
{
    public static class Day01
    {

        public static int Part1(string filePath)
        {
            var input = DataSet(filePath);
            return (from a in input
                    let b = 2020 - a
                    where input.Contains(b)
                    select a * b)
                .First();
        }
        
        public static int Part2(string filePath)
        {
            var input = DataSet(filePath);
            return (from a in input
                    from b in input
                    let c = 2020 - a - b
                    where input.Contains(c)
                    select a * b *c)
                .First();
        }


        public static int Part1_comb(string filePath)
        {
            var m = DataSet(filePath)
                .Combinations(2)
                .First(p => p.Sum() == 2020);
            return m[0] * m[1];
        }

        public static int Part2_comb(string filePath)
        {
            var m = DataSet(filePath)
                    .Combinations(3)
                    .First(p => p.Sum() == 2020);
            return m[0] * m[1] * m[2];
        }
        
        private static HashSet<int> DataSet(string filePath) =>
            File.ReadAllLines(filePath).Select(int.Parse).ToHashSet();


  
    }
}