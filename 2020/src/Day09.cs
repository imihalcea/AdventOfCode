using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2020
{
    public class Day09
    {
        private const string INPUT_FILE_PATH = "test/day09.txt";

        public static long Part1()
        {
            var dataset = Dataset;
            var windowSize = 25;
            var seq = new List<long>(dataset.Take(windowSize));
            foreach (var next in dataset.Skip(windowSize).ToList())
            {
                if (seq.Windowed(windowSize).Combinations(2).All(c => c[0] + c[1] != next))
                    return next;
                seq.Add(next);
            }
            return 0;
        }

        public static long Part2()
        {
            var seq = Dataset;
            var invalidNumber = Part1();
            var (setStart, setStop) = FindContiguousSet(seq, invalidNumber);
            return seq[setStart..setStop].Min() + seq[setStart..setStop].Max();
        }

        private static (int start, int stop) FindContiguousSet(long[] seq, long invalidNumber)
        {
            for (var i = 0; i < seq.Length; i++)
            {
                var sum = 0L; var j=i;
                while (sum<invalidNumber && j<seq.Length)
                {
                    sum += seq[j];
                    if (j-i >= 2 && sum == invalidNumber)
                        return (i, j);
                    j++;
                }
            }
            return (0,0);
        }

        private static long[] Dataset =>
            File.ReadAllLines(INPUT_FILE_PATH).Select(long.Parse).ToArray();
    }

}