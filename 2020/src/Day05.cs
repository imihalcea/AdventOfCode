using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2020
{
    public class Day05
    {
        public static int Part1(string filePath) => Dataset(filePath).Max(Id);

        public static int Part2(string filePath)
        {
            var knownIds = Dataset(filePath).Select(Id).ToArray();
            Array.Sort(knownIds);
            var (min, max) = (knownIds[0], knownIds[^1]);
            return Enumerable.Range(min, max-min+1).Except(knownIds).Single();
        }

        public static int Id(string boardingPass)
        {
            var row = DecodePart(boardingPass.Substring(0, 7), (0, ROWS-1));
            var col = DecodePart(boardingPass.Substring(7, 3), (0, COLS-1));
            return row * COLS + col;
        }

        private static int DecodePart(string part, (int start, int end) startRange) => part.Aggregate(startRange, HalfRange).start;

        private static (int, int) HalfRange((int, int) range, char c) => (c == 'F' || c == 'L') ? LowerHalf(range) : UpperHalf(range);
   
        private static (int, int) LowerHalf((int a, int b) r) => (r.a, r.b - (r.b - r.a) / 2 - 1);
        
        private static (int, int) UpperHalf((int a, int b) r) => (r.a + (r.b - r.a) / 2 + 1, r.b);

        private static IEnumerable<string> Dataset(string filePath) => File.ReadAllLines(filePath);
        
        private const int ROWS = 128, COLS = 8;
    }
}