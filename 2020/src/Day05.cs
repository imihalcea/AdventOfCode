using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2020
{
    public class Day05
    {
        public static int Part1(string filePath) => Dataset(filePath).Max(Decode);

        public static int Decode(string boardingPass)
        {
            var row = DecodePart(boardingPass.Substring(0, 7),(0,127));
            var col = DecodePart(boardingPass.Substring(7, 3),(0,7));
            return row * 8 + col;
        }

        private static int DecodePart(string substring, (int start, int end) range) => 
            substring.Aggregate(range, (acc, n) => ValueOf(acc, n)).start;

        private static (int start,int end) ValueOf((int start,int end) prev, char c) =>
            c switch
            {
                {} when c == 'F' || c == 'L' => LowerHalf(prev),
                {} when c == 'B' || c == 'R' => UpperHalf(prev),
                _ => throw new NotSupportedException()
            };
        
        private static (int,int) LowerHalf((int a, int b) r) => (r.a, r.b -1 - (r.b - r.a) / 2);
        private static (int,int) UpperHalf((int a, int b) r) => (r.a + (r.b - r.a) / 2 + 1, r.b);

        private static IEnumerable<string> Dataset(string filePath) => File.ReadAllLines(filePath);

    }
}