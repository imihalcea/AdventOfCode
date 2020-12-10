using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace _2020
{
    public static class Day10
    {
        private const string INPUT_FILE_PATH = "test/day10.txt";

        public static int Part1()
        {
            var (d1, d3) = CountDiffs(Dataset);
            return d1 * d3;
        }

        public static long Part2() => CountArrangements(Dataset);

        public static (int,int) CountDiffs(int[] input)
        {
            var adapters = input.Append(input.Max() + 3).Append(0).ToImmutableSortedSet();
            int d1 = 0, d3 = 0;
            for (var i = 0; i < adapters.Count - 1; i++)
                switch (adapters[i + 1] - adapters[i])
                {
                    case 1:
                        d1 += 1;
                        break;
                    case 3:
                        d3 += 1;
                        break;
                }
            return (d1,d3);
        }

        public static long CountArrangements(int[] input)
        {
            var adapters = input.Append(0).Append(input.Max() + 3).ToImmutableSortedSet();
            var rollingValues = new long[3]; //keep track of values for i+1,i+2,i+3
            rollingValues[0] = 1L; //starting from the end only one possible way to arrange the adapters
            var i = adapters.Count-2;
            do {
                var sum = 0L;
                for (var diff = 1; diff <= 3; diff++)
                    if (i + diff < adapters.Count && adapters[i + diff] - adapters[i] <= 3)
                        sum += rollingValues[diff - 1];
                
                rollingValues = new[] {sum, rollingValues[0], rollingValues[1]};
                i--;
            } while (i >= 0);
            return rollingValues[0];
        }

        public static int[] Dataset => File.ReadLines(INPUT_FILE_PATH).Select(int.Parse).ToArray();
    }
}