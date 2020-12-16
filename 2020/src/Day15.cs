using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;

namespace _2020
{
    public class Day15
    {
        public static int Part1((int[], int) input)
        {
            var (dataset, n) = input;
            var mem = new ConcurrentDictionary<int,int?[]>();
            for (var i = 0; i < dataset.Length; i++)
            {
                mem[dataset[i]] = new int?[]{i+1,null};
            }
            var lastSpoken = dataset.Last();            
            for (var turn = dataset.Length+1; turn <= n; turn++)
            {
                var pastOccurrences = mem[lastSpoken];
                int next = pastOccurrences[1]==null ? 0 : pastOccurrences[1]!.Value - pastOccurrences[0]!.Value;
                var currentTurn = turn;
                mem.AddOrUpdate(
                    key:next,
                    addValueFactory: _ => new int?[] {currentTurn, null},
                    updateValueFactory: (k, v) =>
                    {
                        Swap(ref v[0], ref v[1]);
                        v[1] = currentTurn;
                        return v;
                    });
                lastSpoken = next;
            }
            return lastSpoken;
        }

        static void Swap(ref int? a, ref int? b)
        {
            if(!b.HasValue) return;
            int? temp = a;
            a = b;
            b = temp;
        }
        public static int[] Dataset(string input) => input.Split(",").Select(int.Parse).ToArray();
    }
}