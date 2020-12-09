using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace _2020
{
    public static class Day01
    {
        public static int Part1(string filePath)
        {
            var m = DataSet(filePath)
                .Combinations(2)
                .First(p => p.Sum() == 2020);
            return m[0] * m[1];
        }

        public static int Part2(string filePath)
        {
            var m = DataSet(filePath)
                    .Combinations(3)
                    .First(p => p.Sum() == 2020);
            return m[0] * m[1] * m[2];
        }
        
        private static int[] DataSet(string filePath) =>
            File.ReadAllLines(filePath).Select(int.Parse).ToArray();


  
    }
}