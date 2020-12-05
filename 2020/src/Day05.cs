using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2020
{
    public class Day05
    {
        public static int Part1(string filePath) => Dataset(filePath).Max(Decode);

        public static int Part2(string filePath)
        {
            var knownIds = Dataset(filePath).Select(Decode).ToArray();
            var rowsToIgnore = (ROWS * COLS - knownIds.Length)/COLS;
            int[] guess;
            do
            {
                guess = Ids(ROWS, COLS,rowsToIgnore).Except(knownIds).ToArray();
                rowsToIgnore++;
            } while (guess.Length!=1);
            return guess.FirstOrDefault()!;
        } 

        public static int Decode(string boardingPass)
        {
            var row = DecodePart(boardingPass.Substring(0, 7), (0, ROWS-1));
            var col = DecodePart(boardingPass.Substring(7, 3), (0, COLS-1));
            return Id(row, col);
        }

        private static IEnumerable<int> Ids(int rows, int cols, int rowsToIgnore)
        {
            var h = rowsToIgnore / 2;
            for (var j = 0; j < cols; j++)
            for (var i = h; i < rows-h; i++)
                yield return Id(i, j);
        }

        private static int Id(int row, int col) => row * COLS + col;

        private static int DecodePart(string part, (int start, int end) range) =>
            part.Aggregate(range, NextRange).start;

        private static (int start, int end) NextRange((int start, int end) range, char c) =>
            (c == 'F' || c == 'L') ? LowerHalf(range) : UpperHalf(range);
   
        private static (int, int) LowerHalf((int a, int b) r) => (r.a, r.b - 1 - (r.b - r.a) / 2);
        private static (int, int) UpperHalf((int a, int b) r) => (r.a + (r.b - r.a) / 2 + 1, r.b);
        private static IEnumerable<string> Dataset(string filePath) => File.ReadAllLines(filePath);
        private const int ROWS = 128;
        private const int COLS = 8;
    }
}