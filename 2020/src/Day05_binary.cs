using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Linq.Enumerable;

namespace _2020
{
    public static class Day05_binary
    {
        public static int Part1(string filePath) => Dataset(filePath).Max(Id);

        public static int Part2(string filePath)
        {
            var knownIds = Dataset(filePath).Select(Id).ToHashSet();
            var (min, max) = (knownIds.Min(), knownIds.Max());
            return Range(min, max-min+1).Except(knownIds).Single();
        }

        public static int Id(string boardingPass) => boardingPass.Select(CharToBit).ToInt32();

        private static int ToInt32(this IEnumerable<char> bits) => Convert.ToInt32(new string(bits.ToArray()),2);

        private static char CharToBit(char c) => (c == 'B' || c == 'R')?'1':'0';

        private static IEnumerable<string> Dataset(string filePath) => File.ReadAllLines(filePath);
    }
}